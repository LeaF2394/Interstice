// (c)2024-2026 Team Pawouch
// RequestExecution.cs - 2026-11-21 22:11:29

#nullable enable
using System.Collections;
using System.Threading;
using com.team_pawouch.framework.core.certificates.api;
using com.team_pawouch.framework.core.datas.api;

namespace com.team_pawouch.framework.core.certificates;

public sealed class RequestExecution(Func<IData, CancellationToken, IEnumerator>? factory) : IExecutable {
	public static IExecutable Iterator(Func<IData, CancellationToken, IEnumerator> factory)
		=> new RequestExecution(factory ?? throw new ArgumentNullException(nameof(factory)));

	public Func<IData, CancellationToken, IEnumerator> Execute() => factory ?? throw new NullReferenceException();
}
