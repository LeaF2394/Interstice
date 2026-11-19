// (c)2024-2026 Team Pawouch
// IConstructable.cs - 2026-11-20 00:11:52

#nullable enable
namespace com.team_pawouch.framework.core.behaviour.api;

public interface IConstructable {
	void Construct(IParameterSet? parameters = null);
	void Deconstruct();
}

// END FILE
