// (c)2024-2026 Team Pawouch
// IQueueToken.cs - 2026-11-21 20:11:58

#nullable enable
using System.Threading;
using com.team_pawouch.framework.common.api;

namespace com.team_pawouch.framework.core.certificates.api;

public interface IQueueToken : IIdentifier {
	IRequest          Request      { get; }
	IRequestHandler   Handler      { get; }
	CancellationToken Cancellation { get; }

	void Report(RequestStatus status, string? failureReason);
}
