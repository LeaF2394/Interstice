// (c)2024-2026 Team Pawouch
// IData.cs - 2026-11-21 14:11:54

#nullable enable
using System.Collections.Generic;

namespace com.team_pawouch.framework.core.datas.api;

public interface IData : IJSON {
	IReadOnlyDictionary<string, IPropertyEntry<object>> Properties { get; }
	object this[string name] { get; }

	event Action<IPropertyEntry<object>> OnPropertyChanged;

	void    AddProperty(string    name, object      value) => AddProperty<object>(name, value);
	bool    TryAddProperty(string name, object      value) => TryAddProperty<object>(name, value);
	bool    RemoveProperty(string name, out object? value) => RemoveProperty<object>(name, out value);
	object? GetProperty(string    name)                    => GetProperty<object>(name);
	bool    TryGetProperty(string name, out object? value) => TryGetProperty<object>(name, out value);
	void    SetProperty(string    name, object      value) => SetProperty<object>(name, value);
	bool    TrySetProperty(string name, object      value) => TrySetProperty<object>(name, value);

	void AddProperty<T>(string    name, T value);
	bool TryAddProperty<T>(string name, T value);
	bool RemoveProperty(string    name);
	bool RemoveProperty<T>(string name, out T? value);
	T?   GetProperty<T>(string    name);
	bool TryGetProperty<T>(string name, out T? value);
	void SetProperty<T>(string    name, T      value);
	bool TrySetProperty<T>(string name, T      value);
	void Clear();
}

// END FILE
