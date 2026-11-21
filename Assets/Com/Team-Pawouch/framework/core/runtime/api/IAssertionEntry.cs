// (c)2024-2026 Team Pawouch
// IAssertionEntry.cs - 2026-11-21 10:11:34

#nullable enable
namespace com.team_pawouch.framework.core.runtime.api;

public interface IAssertionEntry : ILogEntry {
	LogSeverity ILogEntry.Severity  => LogSeverity.Assert;
	bool                  Condition { get; }
}

// END FILE
