// (c)2024-2026 Team Pawouch
// ICastable.cs - 2026-11-20 00:11:23

#nullable enable
namespace com.team_pawouch.framework.core.datas.api;

public interface ICastable<T> {
	T? Value { get; }

	U? CastTo<U>();
	T? CastFrom<U>(U from);

	bool CanCastTo<U>();
	bool CanCastFrom<U>();

	void AddCastTo<U>(Func<T, U?> cast);
	bool RemoveCastTo<U>();
	void AddCastFrom<U>(Func<U, T?> cast);
	bool RemoveCastFrom<U>();
	void Clear();
}

// END FILE
