// (c)2024-2026 Team Pawouch
// IExecutable.cs - 2026-11-19 02:11:18

#nullable enable

namespace com.team_pawouch.framework.core.api;

public interface IExecutable {
	IExecutable Then(IExecutable      exec);
	IRequest    Request(IParameterSet parameters);
}

// END FILE
