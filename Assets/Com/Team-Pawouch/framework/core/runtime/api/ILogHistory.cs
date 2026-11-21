// (c)2024-2026 Team Pawouch
// ILogHistory.cs - 2026-11-21 11:11:52

#nullable enable
namespace com.team_pawouch.framework.core.runtime.api;

public interface ILogHistory {
	void         Add(ILogEntry entry);
	ILogEntry[]? Lookup(string subject);
	ILogEntry[]? Lookup(string subject, DateTime until);
}

// END FILE
