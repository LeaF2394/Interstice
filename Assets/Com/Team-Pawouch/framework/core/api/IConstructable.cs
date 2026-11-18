// (c)2024-2026 Team Pawouch
// IConstructable.cs - 2026-11-19 02:11:01

#nullable enable
namespace com.team_pawouch.framework.core.api;

public interface IConstructable {
	void Construct(IParameterSet? parameters = null);
	void Deconstruct();
}

// END FILE
