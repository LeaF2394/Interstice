// (c)2024-2026 Team Pawouch
// ILogEntry.cs - 2026-11-21 10:11:10

#nullable enable
namespace com.team_pawouch.framework.core.runtime.api;

public interface ILogEntry {
	DateTime    Timestamp { get; }
	LogSeverity Severity  { get; }
	string      Subject   { get; }
	string      Message   { get; }
}

// END FILE
