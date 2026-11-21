// (c)2024-2026 Team Pawouch
// IQueueRouter.cs - 2026-11-21 20:11:45

#nullable enable
namespace com.team_pawouch.framework.core.certificates.api;

public interface IQueueRouter {
	IQueueKey Route(IRequest request);
}
