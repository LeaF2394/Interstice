// (c)2024-2026 Team Pawouch
// RequestExtensions.cs - 2026-11-21 20:11:43

#nullable enable
using com.team_pawouch.framework.core.certificates.api;

namespace com.team_pawouch.framework.core.certificates.extensions;

public static class RequestExtensions {
	public static IRequestState? GetState(this IRequest request, IRequestDispatcher dispatcher) => dispatcher.PullState(request.GUID);

	public static bool IsTerminal(this IRequestState state) => state.Status is RequestStatus.Succeeded or RequestStatus.Failed or RequestStatus.Cancelled;
}

// END FILE
