// (c)2024-2026 Team Pawouch
// Bootstrap.cs - 2026-11-22 15:11:03

#nullable enable
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Package.Com.TeamPawouch.Interstice.Runtime.Core.Application.Adapter;
using Package.Com.TeamPawouch.Interstice.Runtime.Core.Kernel.Interpreter.Lua;
using Package.Com.TeamPawouch.Interstice.Shared;

namespace Package.Com.TeamPawouch.Interstice.Runtime.Core.Application;

internal static class Bootstrap {
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void Complete() {
		Singleton<AppPath>.CreateInstance(() => new AppPath());
		Singleton<AppPath>.ClassInstance!.BasePath = Path.GetFullPath(Path.Combine(UnityEngine.Application.dataPath, ".."));
		Singleton<AppPath>.ClassInstance.GamePath  = Path.Combine(Singleton<AppPath>.ClassInstance.BasePath, $".{UnityEngine.Application.productName.ToLower()}");

		Directory.CreateDirectory(Singleton<AppPath>.ClassInstance.GamePath);
		Directory.CreateDirectory(Singleton<AppPath>.ClassInstance.ModPath);
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	private static void Main() {
		Singleton<GameLauncher>.CreateInstance(() => new GameLauncher(Environment.GetCommandLineArgs()));
		Task.Run(() => Singleton<GameLauncher>.ClassInstance!.Launch(), CancellationToken.None);
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Adapt() {
		UnityRuntime.CreateInstance().Initialize();
		UnityRuntime.ClassInstance?.StartProcess();

		LuaInterpreter.Installation(UnityRuntime.ClassInstance);

		UnityEngine.Application.quitting -= LuaInterpreter.Uninstallation;
		UnityEngine.Application.quitting += LuaInterpreter.Uninstallation;
		UnityEngine.Application.quitting -= OnQuit;
		UnityEngine.Application.quitting += OnQuit;

		LuaInterpreter.Interpret("main");
		return;

		void OnQuit() {
			if(UnityRuntime.ClassInstance == null) return;

			UnityRuntime.ClassInstance.Dispose();
		}
	}
}

// END FILE
