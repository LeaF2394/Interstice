// (c)2024-2026 Team Pawouch
// RequestSystemBuilder.cs - 2026-11-21 22:11:50

#nullable enable
using System.Threading;
using com.team_pawouch.framework.core.certificates.api;

namespace com.team_pawouch.framework.core.certificates;

public sealed class RequestSystemBuilder {
	private IRequestStateStorage?          _store;
	private IQueueRouter?                  _router;
	private Func<CancellationTokenSource>? _ctsFactory;
	private IRequestRecorder?              _recorder;

	public RequestSystemBuilder WithStore(IRequestStateStorage store) {
		_store = store;
		return this;
	}

	public RequestSystemBuilder WithRouter(IQueueRouter router) {
		_router = router;
		return this;
	}

	public RequestSystemBuilder WithCancellationTokenSourceFactory(Func<CancellationTokenSource> f) {
		_ctsFactory = f;
		return this;
	}

	public RequestSystemBuilder WithRecorder(IRequestRecorder recorder) {
		_recorder = recorder;
		return this;
	}

	public (IRequestDispatcher dispatcher, IRequestDriver runner, IRequestStateStorage store) Build() {
		IRequestStateStorage          store      = _store      ?? new RequestStateStorage(_recorder);
		IQueueRouter                  router     = _router     ?? new QueueRouter();
		Func<CancellationTokenSource> cts        = _ctsFactory ?? ( () => new CancellationTokenSource() );
		RequestDispatcher             dispatcher = new(store, router, cts);
		RequestDriver                 runner     = new(dispatcher);
		return ( dispatcher, runner, store );
	}
}
