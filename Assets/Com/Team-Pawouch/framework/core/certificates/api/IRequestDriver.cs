// (c)2024-2026 Team Pawouch
// IRequestDriver.cs - 2026-11-21 20:11:12

#nullable enable
using System.Collections;
using com.team_pawouch.framework.core.datas.api;

namespace com.team_pawouch.framework.core.certificates.api;

public interface IRequestDriver {
	IEnumerator? Pump(IQueueKey key, IData parameters);
}
