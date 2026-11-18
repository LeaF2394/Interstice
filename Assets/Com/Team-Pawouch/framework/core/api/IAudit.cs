// (c)2024-2026 Team Pawouch
// IAudit.cs - 2026-11-19 03:11:07

#nullable enable
namespace com.team_pawouch.framework.core.api;

public enum AuditState {
	None,
	Pending,
	Pass,
	Rejected,
}

public interface IAudit {
	long       Timestamp { get; }
	string     Display   { get; }
	AuditState State     { get; }
}

// END FILE
