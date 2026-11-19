// (c)2024-2026 Team Pawouch
// IIndexer.cs - 2026-11-20 00:11:00

#nullable enable
namespace com.team_pawouch.framework.core.datas.api;

public interface IIndexer<in K, V> {
	V? this[K index] { get; set; }
}

// END FILE

