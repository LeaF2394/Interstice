// (c)2024-2026 Team Pawouch
// Function.cs - 2026-11-19 03:11:06

#nullable enable
using com.team_pawouch.framework.core.api;

namespace com.team_pawouch.framework.core;

public readonly record struct Function<T> : IFunction<T> {
	private IRequest                _requestId { get; }
	private Func<IParameterSet, T>? _expr      { get; init; }

	public Function() => throw new InvalidOperationException();

	private Function(IRequest requestId, Func<IParameterSet, T>? expr) {
		_requestId = requestId;
		_expr      = expr ?? throw new ArgumentNullException(nameof(expr));
	}

	public static IFunction<T> Request(Func<IParameterSet, T> expr) => new Function<T>(default, expr);

	public Func<IParameterSet, T>? Execution() => _expr;

	public ISubscribe<Func<IParameterSet, T>?> Subscribe(Func<IParameterSet, T>? subscription) => this with {
		_expr = _expr - subscription + subscription
	};

	public ISubscribe<Func<IParameterSet, T>?> Unsubscribe(Func<IParameterSet, T>? unsubscription) => this with {
		_expr = _expr - unsubscription
	};
}


// END FILE
