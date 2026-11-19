// (c)2024-2026 Team Pawouch
// IClassMeta.cs - 2026-11-20 00:11:29

#nullable enable
namespace com.team_pawouch.framework.core.behaviour.api;

public interface IClassMeta {
	IClassInstance? ClassInstance { get; set; }
	IClassBuilder?  ClassBuilder  { get; set; }
	bool            IsEmpty       => ClassInstance == null;
}
