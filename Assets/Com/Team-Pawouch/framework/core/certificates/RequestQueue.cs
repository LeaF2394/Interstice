// (c)2024-2026 Team Pawouch
// RequestQueue.cs - 2026-11-21 22:11:06

#nullable enable
using System.Collections.Generic;
using System.Threading;
using com.team_pawouch.framework.core.certificates.api;

namespace com.team_pawouch.framework.core.certificates;

public sealed class RequestQueue(IQueueKey key, IRequestStateWriter writer, Func<CancellationTokenSource> ctsFactory) : IRequestQueue {
	public Guid      GUID => Key.GUID;
	public IQueueKey Key  { get; } = key;

	public int Count {
		get {
			lock(_gate) return _pending.Count + ( _running is null? 0 : 1 );
		}
	}

	public bool IsReady {
		get {
			lock(_gate) return _running is null && _pending.First is not null;
		}
	}

	private readonly CancellationTokenSource _cts     = ctsFactory();
	private readonly LinkedList<IQueueToken> _pending = new();
	private readonly object                  _gate    = new();
	private          IQueueToken?            _running;

	public IQueueToken Enqueue(IRequest request, IRequestHandler handler) {
		IQueueToken token;
		lock(_gate) {
			token = new Token(request, handler, _cts.Token, this);
			_pending.AddLast(token);
		}

		writer.MarkEnqueued(request.GUID, request.Subject);
		return token;
	}

	public IQueueToken? Dequeue() {
		lock(_gate) {
			if(_running is not null || _pending.First is null) return null;
			LinkedListNode<IQueueToken> node = _pending.First;
			_pending.RemoveFirst();
			_running = node.Value;
			return _running;
		}
	}

	public bool Cancel(Guid id) {
		lock(_gate) {
			for(LinkedListNode<IQueueToken>? n = _pending.First;n is not null;n = n.Next) {
				if(n.Value.Request.GUID != id) continue;
				_pending.Remove(n);
				writer.MarkCanceled(id, n.Value.Request.Subject);

				return true;
			}
		}

		return false;
	}

	internal void Complete(IQueueToken ticket, RequestStatus status, string? reason) {
		lock(_gate) {
			if(ReferenceEquals(_running, ticket)) _running = null;
		}

		switch(status) {
			case RequestStatus.Succeeded: writer.MarkSucceeded(ticket.Request.GUID, ticket.Request.Subject); break;
			case RequestStatus.Failed:    writer.MarkFailed(ticket.Request.GUID, ticket.Request.Subject, reason ?? "failed"); break;
			case RequestStatus.Cancelled: writer.MarkCanceled(ticket.Request.GUID, ticket.Request.Subject); break;
		}
	}

	internal void AbortAll() {
		List<IQueueToken> snapshot;
		lock(_gate) {
			snapshot = [.. _pending];
			_pending.Clear();
		}

		foreach(IQueueToken t in snapshot) writer.MarkCanceled(t.Request.GUID, t.Request.Subject);
	}

	internal sealed class Token(IRequest request, IRequestHandler handler, CancellationToken ct, IRequestQueue owner) : IQueueToken {
		public Guid              GUID         => Request.GUID;
		public IRequest          Request      { get; } = request;
		public IRequestHandler   Handler      { get; } = handler;
		public CancellationToken Cancellation { get; } = ct;

		public void Report(RequestStatus status, string? message) => ( owner as RequestQueue )?.Complete(this, status, message);
	}
}
