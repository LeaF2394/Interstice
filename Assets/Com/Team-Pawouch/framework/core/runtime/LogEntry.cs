// (c)2024-2026 Team Pawouch
// LogEntry.cs - 2026-11-21 10:11:51

#nullable enable
using com.team_pawouch.framework.core.runtime.api;

namespace com.team_pawouch.framework.core.runtime;

public record LogEntry(DateTime Timestamp, LogSeverity Severity, string Subject, string Message) : ILogEntry {
	public override string ToString() => $"{Timestamp:HH:mm:ss.fff} [{Severity.ToString()}] {Subject}: {Message}";
}

// END FILE
