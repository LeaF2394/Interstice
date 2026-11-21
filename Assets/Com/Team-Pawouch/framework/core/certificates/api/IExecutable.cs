// (c)2024-2026 Team Pawouch
// IExecutable.cs - 2026-11-21 20:11:43

#nullable enable
using System.Collections;
using System.Threading;
using com.team_pawouch.framework.core.datas.api;

namespace com.team_pawouch.framework.core.certificates.api;

public interface IExecutable {
	Func<IData, CancellationToken, IEnumerator> Execute();
}

// END FILE
