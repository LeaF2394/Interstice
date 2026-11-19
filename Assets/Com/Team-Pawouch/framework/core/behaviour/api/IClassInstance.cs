// (c)2024-2026 Team Pawouch
// IClassInstance.cs - 2026-11-20 00:11:40

#nullable enable
using com.team_pawouch.framework.core.certificates.api;

namespace com.team_pawouch.framework.core.behaviour.api;

public interface IClassInstance : IIdentifier, IConstructable {
	Type   Type { get; }
	string Name { get; }
}
