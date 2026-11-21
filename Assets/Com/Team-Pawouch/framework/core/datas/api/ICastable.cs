// (c)2024-2026 Team Pawouch
// ICastable.cs - 2026-11-21 14:11:50

#nullable enable
namespace com.team_pawouch.framework.core.datas.api;

public interface ICastable<T> {
	T?   Cast<U>(U from);
	U?   As<U>();
	bool TryCast<U>(U    cast, out T? result);
	bool TryAs<U>(out U? result);

	void ProvideAs<U>(Func<T, U>   method);
	void ProvideCast<U>(Func<U, T> method);
}

// END FILE
