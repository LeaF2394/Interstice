// (c)2024-2026 Team Pawouch
// AssertionEntry.cs - 2026-11-21 10:11:39

#nullable enable
using com.team_pawouch.framework.core.runtime.api;

namespace com.team_pawouch.framework.core.runtime;

public record AssertionEntry(DateTime Timestamp, string Subject, string Message, bool Condition) : IAssertionEntry {
	public DateTime Timestamp { get; } = Timestamp;
	public string   Subject   { get; } = Subject;
	public string   Message   { get; } = Message;
	public bool     Condition { get; } = Condition;

	public override string ToString() => $"{Timestamp:HH:mm:ss.fff} [Assert] #{Condition} {Subject}: {Message}";
}

// END FILE
