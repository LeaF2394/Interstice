// (c)2024-2026 Team Pawouch
// ISubscribe.cs - 2026-11-19 02:11:01

#nullable enable
namespace com.team_pawouch.framework.core.api;

public interface ISubscribe<in T> {
	ISubscribe<T> Subscribe(T   subscription);
	ISubscribe<T> Unsubscribe(T unsubscription);
}

// END FILE
