// (c)2024-2026 Team Pawouch
// ISet.cs - 2026-11-20 00:11:15

#nullable enable
using System.Collections.Generic;

namespace com.team_pawouch.framework.core.datas.api;

public interface ISet<K, V> {
	IReadOnlyDictionary<K, V> ReadOnly { get; }

	void Add(K         key, V value);
	bool TryAdd(K      key, V value);
	bool Remove(K      key);
	bool TryGetValue(K key, out V? value);
	void Clear();
}

// END FILE
