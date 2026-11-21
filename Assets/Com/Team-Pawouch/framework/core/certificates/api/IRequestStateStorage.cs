// (c)2024-2026 Team Pawouch
// IRequestStateStorage.cs - 2026-11-21 20:11:57

#nullable enable
namespace com.team_pawouch.framework.core.certificates.api;

public interface IRequestStateStorage {
	IRequestState?      TryGet(Guid id);
	IRequestStateWriter Construct();
}
