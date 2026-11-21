// (c)2024-2026 Team Pawouch
// RequestStatus.cs - 2026-11-21 20:11:45

#nullable enable
namespace com.team_pawouch.framework.core.certificates.api;

public enum RequestStatus {
	Generated,
	Enqueued,
	Solving,
	Succeeded,
	Failed,
	Cancelled,
}
