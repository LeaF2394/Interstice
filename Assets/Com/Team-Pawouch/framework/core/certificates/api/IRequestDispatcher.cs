// (c)2024-2026 Team Pawouch
// IRequestDispatcher.cs - 2026-11-21 20:11:39

#nullable enable
using System.Collections.Generic;

namespace com.team_pawouch.framework.core.certificates.api;

public interface IRequestDispatcher {
	IReadOnlyCollection<IQueueKey> ActiveQueueKeys { get; }

	Guid           Dispatch(IRequest     request, IRequestHandler handler);
	Guid           Dispatch(IRequest     request, IQueueKey       key, IRequestHandler handler);
	IQueueToken?   PullDequeue(IQueueKey key);
	IRequestState? PullState(Guid        id);
	bool           TryCancel(Guid        id);
}
