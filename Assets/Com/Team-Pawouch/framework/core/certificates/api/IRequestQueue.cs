// (c)2024-2026 Team Pawouch
// IRequestQueue.cs - 2026-11-21 20:11:31

#nullable enable
using com.team_pawouch.framework.common.api;

namespace com.team_pawouch.framework.core.certificates.api;

public interface IRequestQueue : IIdentifier {
	IQueueKey Key     { get; }
	int       Count   { get; }
	bool      IsReady { get; }

	IQueueToken  Enqueue(IRequest request, IRequestHandler handler);
	IQueueToken? Dequeue();
	bool         Cancel(Guid id);
}
