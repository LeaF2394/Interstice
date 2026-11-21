// (c)2024-2026 Team Pawouch
// IRequest.cs - 2026-11-21 16:11:51

#nullable enable
using com.team_pawouch.framework.common.api;

namespace com.team_pawouch.framework.core.certificates.api;

public interface IRequest : IIdentifier {
	string Subject { get; }
}

// END FILE
