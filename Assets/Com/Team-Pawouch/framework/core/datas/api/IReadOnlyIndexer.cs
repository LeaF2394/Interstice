// (c)2024-2026 Team Pawouch
// IReadOnlyIndexer.cs - 2026-11-20 00:11:07

#nullable enable
namespace com.team_pawouch.framework.core.datas.api;

public interface IReadOnlyIndexer<in K, out V> {
	V? this[K index] { get; }
}
