// (c)2024-2026 Team Pawouch
// IPropertyEntry.cs - 2026-11-21 14:11:43

#nullable enable
namespace com.team_pawouch.framework.core.datas.api;

public interface IPropertyEntry : ICastable<IPropertyEntry>, IJSON {
	string  Name     { get; }
	object? Value    { get; }
	bool    Constant { get; }
}

public interface IPropertyEntry<T> : ICastable<IPropertyEntry<T>>, IJSON {
	string Name     { get; }
	T?     Value    { get; }
	bool   Constant { get; }
}

// END FILE
