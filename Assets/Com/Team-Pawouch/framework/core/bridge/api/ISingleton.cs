// (c)2024-2026 Team Pawouch
// ISingleton.cs - 2026-11-21 14:11:15

#nullable enable
using com.team_pawouch.framework.common.api;
using com.team_pawouch.framework.core.datas.api;

namespace com.team_pawouch.framework.core.bridge.api;

public interface ISingleton<out T> : IIdentifier where T : ISingleton<T> {
	T    Installation();
	T    Installation(params object[] args);
	T    Installation(IData           settings);
	void Uninstallation();
}

// END FILE
