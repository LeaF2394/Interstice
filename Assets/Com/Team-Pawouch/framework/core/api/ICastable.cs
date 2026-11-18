// (c)2024-2026 Team Pawouch
// ICastable.cs - 2026-11-19 02:11:14

#nullable enable
namespace com.team_pawouch.framework.core.api;

public interface ICastable<T> {
	T? Value { get; }

	U? CastTo<U>();
	T? CastFrom<U>(U from);

	void AddCastTo<U>(Func<T, U?> cast);
	bool RemoveCastTo<U>();
	void AddCastFrom<U>(Func<U, T?> cast);
	bool RemoveCastFrom<U>();
}

// END FILE
