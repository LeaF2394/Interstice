// (c)2024-2026 Team Pawouch
// IRequestState.cs - 2026-11-20 02:11:15

#nullable enable
namespace com.team_pawouch.framework.core.certificates.api;

public interface IRequestState {
	public enum State {
		None,
		Pending,
		Success,
		Failure,
		NoOperation,
	}

	State  RequestState { get; }
	object Response     { get; }
}

// END FILE
