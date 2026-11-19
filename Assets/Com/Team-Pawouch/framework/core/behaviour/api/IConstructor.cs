// (c)2024-2026 Team Pawouch
// IConstructor.cs - 2026-11-20 01:11:11

#nullable enable
namespace com.team_pawouch.framework.core.behaviour.api;

public interface IConstructor {
	IConstructable Construct(IParameterSet? parameters = null);
}

// END FILE

