// (c)2024-2026 Team Pawouch
// IExecutor.cs - 2026-11-19 02:11:40

#nullable enable
namespace com.team_pawouch.framework.core.api;

public interface IExecutor<T> {
	T Execute(IExecution<T> execution, IParameterSet parameters);
}

// END FILE
