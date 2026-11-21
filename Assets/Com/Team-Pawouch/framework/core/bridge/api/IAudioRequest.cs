// (c)2024-2026 Team Pawouch
// IAudioRequest.cs - 2026-11-21 18:11:43

#nullable enable
using com.team_pawouch.framework.core.certificates.api;

namespace com.team_pawouch.framework.core.bridge.api;

public interface IAudioRequest : IRequest {
	string File { get; }
}

// END FILE
