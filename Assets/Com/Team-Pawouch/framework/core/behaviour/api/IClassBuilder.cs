// (c)2024-2026 Team Pawouch
// IClassBuilder.cs - 2026-11-20 00:11:37

#nullable enable
namespace com.team_pawouch.framework.core.behaviour.api;

public interface IClassBuilder : IConstructor {
	IConstructable IConstructor.Construct(IParameterSet? parameters) => Build(parameters);
	IClassInstance              Build(IParameterSet?     parameters = null);
}

// END FILE
