// (c)2024-2026 Team Pawouch
// IClassLoader.cs - 2026-11-20 00:11:14

#nullable enable
using System.Collections.Generic;
using System.IO;

namespace com.team_pawouch.framework.core.behaviour.api;

public interface IClassLoader : IConstructable {
	IClassMeta Meta { get; }

	sealed string? Classpath {
		get {
			Type?   type     = Meta.ClassInstance?.Type;
			string? fullName = type?.FullName;

			if(fullName == null) return null;

			List<string> parts = [.. fullName.ToLowerInvariant().Split('.')];
			parts.Insert(0, ClasspathBase);

			return Path.Combine([.. parts]);
		}
	}

	static string ClasspathBase { get; set; } = Path.Combine(Application.persistentDataPath, "bin", "class");

	void IConstructable.Construct(IParameterSet? parameters) => New(parameters);
	void IConstructable.Deconstruct()                        => Release();

	sealed void New(IParameterSet? parameters) {
		if(!Meta.IsEmpty) throw new InvalidOperationException();
		if(Meta.ClassBuilder == null) throw new InvalidOperationException("No builder for class is available");

		Meta.ClassInstance = Meta.ClassBuilder.Build(parameters);
	}

	sealed void Release() {
		if(Meta.IsEmpty) throw new InvalidOperationException();

		Meta.ClassInstance?.Deconstruct();
		Meta.ClassInstance = null;
	}

	sealed void Load(IClassInstance instance) {
		Meta.ClassInstance?.Deconstruct();
		Meta.ClassInstance = instance;
	}
}

// END FILE
