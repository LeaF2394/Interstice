// (c)2024-2026 Team Pawouch
// QueueRouter.cs - 2026-11-21 22:11:44

#nullable enable
using System.Collections.Generic;
using com.team_pawouch.framework.core.certificates.api;

namespace com.team_pawouch.framework.core.certificates;

public sealed class QueueRouter : IQueueRouter {
	public static IQueueRouter Instance {
		get {
			_router ??= new QueueRouter();
			return _router;
		}
	}

	private static IQueueRouter? _router;

	private readonly Dictionary<string, IQueueKey> _routes = new();
	private readonly object                        _gate   = new();

	public IQueueKey Route(IRequest request) {
		IQueueKey nKey;
		lock(_gate) {
			if(_routes.TryGetValue(request.Subject, out IQueueKey key)) return key;
			nKey                     = new QueueKey(Guid.Empty, request.Subject);
			_routes[request.Subject] = nKey;
		}

		return nKey;
	}
}
