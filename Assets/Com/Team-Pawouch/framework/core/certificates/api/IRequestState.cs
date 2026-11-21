// (c)2024-2026 Team Pawouch
// IRequestState.cs - 2026-11-21 20:11:51

#nullable enable
using com.team_pawouch.framework.common.api;

namespace com.team_pawouch.framework.core.certificates.api;

public interface IRequestState : IIdentifier {
	RequestStatus  Status         { get; }
	DateTimeOffset Updated        { get; }
	string?        FailureMessage { get; }
}
