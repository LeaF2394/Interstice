// (c)2024-2026 Team Pawouch
// IExecution.cs - 2026-11-19 02:11:13

#nullable enable
namespace com.team_pawouch.framework.core.api;

public interface IExecution<T> : ISubscribe<Func<IParameterSet, T>?> {
	Func<IParameterSet, T>? Execution();
}

// END FILE
