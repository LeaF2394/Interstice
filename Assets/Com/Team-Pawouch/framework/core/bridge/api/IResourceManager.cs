// (c)2024-2026 Team Pawouch
// IResourceManager.cs - 2026-11-21 14:11:48

#nullable enable
using Object = UnityEngine.Object;

namespace com.team_pawouch.framework.core.bridge.api;

public interface IResourceManager : ISingleton<IResourceManager> {
	void RequestLoad<T>(string      address, Action<Guid, T> onLoaded, Action<Exception> onError) where T : Object;
	void RequestLoad<T>(Guid        id,      Action<Guid, T> onLoaded, Action<Exception> onError) where T : Object;
	void RequestLoadExternal<T>(Uri uri,     Action<Guid, T> onLoaded, Action<Exception> onError) where T : Object;

	void RequestDownload(Uri uri, string store, Action<IProgress<float>> progress, Action complete, Action<Exception> onError);
}

// END FILE
