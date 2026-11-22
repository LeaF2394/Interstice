// (c)2024-2026 Team Pawouch
// Bootstrap.cs - 2026-11-22 15:11:03

#nullable enable
using System.IO;
using Package.Com.TeamPawouch.Interstice.Runtime.Core.Application.Adapter;
using Package.Com.TeamPawouch.Interstice.Runtime.Core.Application.Addon;

namespace Package.Com.TeamPawouch.Interstice.Runtime.Core.Application;

internal static class Bootstrap {
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void Complete() {
		string gameRoot = $".{UnityEngine.Application.productName.ToLower()}";
		string rootPath = Path.GetFullPath(Path.Combine(UnityEngine.Application.dataPath, ".."));

		Directory.CreateDirectory(Path.Combine(rootPath, gameRoot));
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	private static void Main() {
		string[] args = Environment.GetCommandLineArgs();
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Adapt() {
		UnityRuntime.CreateInstance().Initialize();
		UnityRuntime.ClassInstance?.StartProcess();
		
		LuaInterpreter.Installation(UnityRuntime.ClassInstance ?? throw new NullReferenceException());
		LuaInterpreter.Interpret("print('hello')");
	}
}

// END FILE
