// (c)2024-2026 Team Pawouch
// RequestStateSnapshot.cs - 2026-11-21 22:11:18

#nullable enable
using com.team_pawouch.framework.core.certificates.api;

namespace com.team_pawouch.framework.core.certificates;

public sealed record RequestStateSnapshot(
	Guid           GUID,
	string         Subject,
	RequestStatus  Status,
	string?        FailureMessage,
	DateTimeOffset Updated
) : IRequestState {
	public RequestStateSnapshot With(RequestStatus status, string? message)
		=> this with { Status = status, FailureMessage = message, Updated = DateTimeOffset.UtcNow };
}
