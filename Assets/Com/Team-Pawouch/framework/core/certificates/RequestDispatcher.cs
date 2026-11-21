// (c)2024-2026 Team Pawouch
// RequestDispatcher.cs - 2026-11-21 21:11:59

#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using com.team_pawouch.framework.core.certificates.api;

namespace com.team_pawouch.framework.core.certificates;

public sealed class RequestDispatcher(IRequestStateStorage storage, IQueueRouter router, Func<CancellationTokenSource> ctsFactory) : IRequestDispatcher {
	public IReadOnlyCollection<IQueueKey> ActiveQueueKeys {
		get {
			lock(_gate) return [.. _queues.Values.Select(q => q.Key)];
		}
	}

	private readonly IRequestStateWriter             _writer = storage.Construct();
	private readonly Dictionary<Guid, IRequestQueue> _queues = new();
	private readonly object                          _gate   = new();

	public Guid Dispatch(IRequest request, IRequestHandler handler) {
		IQueueKey key = router.Route(request);
		return Dispatch(request, key, handler);
	}

	public Guid Dispatch(IRequest request, IQueueKey key, IRequestHandler handler) {
		if(request == null) throw new ArgumentNullException(nameof(request));
		if(key     == null) throw new ArgumentNullException(nameof(key));

		IRequestQueue queue = GetOrCreateQueue(key);
		queue.Enqueue(request, handler);
		return request.GUID;
	}

	public IQueueToken? PullDequeue(IQueueKey key) {
		IRequestQueue? queue;

		lock(_gate) _queues.TryGetValue(key.GUID, out queue);
		IQueueToken? token = queue?.Dequeue();
		if(token != null) _writer.MarkSolving(token.Request.GUID, token.Request.Subject);
		return token;
	}

	public IRequestState? PullState(Guid id) => storage.TryGet(id);

	public bool TryCancel(Guid id) {
		lock(_gate) return _queues.Values.Any(queue => queue.Cancel(id));
	}

	private IRequestQueue GetOrCreateQueue(IQueueKey key) {
		lock(_gate) {
			if(_queues.TryGetValue(key.GUID, out IRequestQueue existing)) return existing;
			RequestQueue queue = new(key, _writer, ctsFactory);
			_queues[key.GUID] = queue;
			return queue;
		}
	}
}
