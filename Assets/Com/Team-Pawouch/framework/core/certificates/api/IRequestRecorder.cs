// (c)2024-2026 Team Pawouch
// IRequestRecorder.cs - 2026-11-20 02:11:02

#nullable enable
namespace com.team_pawouch.framework.core.certificates.api;

public interface IRequestRecorder {
	IRequestRecord? GetRequest(IRequest requestRef);
	IRequestRecord? GetRequest(Guid     requestId);
}

// END FILE
