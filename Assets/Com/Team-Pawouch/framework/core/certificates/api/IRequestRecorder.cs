// (c)2024-2026 Team Pawouch
// IRequestRecorder.cs - 2026-11-21 20:11:07

#nullable enable
namespace com.team_pawouch.framework.core.certificates.api;

public interface IRequestRecorder {
	void Record(IRequestState snapshot);
}
