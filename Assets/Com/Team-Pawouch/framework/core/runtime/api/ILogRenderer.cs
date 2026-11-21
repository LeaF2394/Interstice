// (c)2024-2026 Team Pawouch
// ILogRenderer.cs - 2026-11-21 10:11:00

#nullable enable
namespace com.team_pawouch.framework.core.runtime.api;

public interface ILogRenderer {
	[HideInCallstack]
	void Render(ILogEntry entry);
}

// END FILE
