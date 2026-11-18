// (c)2024-2026 Team Pawouch
// IIndexer.cs - 2026-11-19 02:11:53

#nullable enable
namespace com.team_pawouch.framework.core.api;

public interface IReadOnlyIndexer<in K, out V> {
	public V? this[K index] { get; }
}

public interface IIndexer<in K, V> {
	public V? this[K index] { get; set; }
}

// END FILE
