// (c)2024-2026 Team Pawouch
// LuaInterpreter.cs - 2026-11-23 03:11:59

#nullable enable
using Package.Com.TeamPawouch.Interstice.Runtime.Core.Application.Api;
using XLua;

namespace Package.Com.TeamPawouch.Interstice.Runtime.Core.Application.Addon;

public static class LuaInterpreter {
	private static int       _instance;
	private static IRuntime? _runtime;
	private static LuaEnv?   _env;

	public static void Installation(IRuntime runtime) {
		if(_instance > 0) {
			SelfCleanup();
		}

		_instance++;
		_runtime = runtime;
		_env     = new LuaEnv();

		_runtime.OnTick -= OnTick;
		_runtime.OnTick += OnTick;
	}

	private static void OnTick(int tick) => _env?.Tick();

	private static void SelfCleanup() {
		_instance = 0;

		if(_runtime != null) _runtime.OnTick -= OnTick;

		_env?.Dispose();
		_env     = null;
		_runtime = null;
	}

	public static void Interpret(string code) {
		if(_env is null) throw new NullReferenceException();

		_env.DoString(code);
	}
}

// END FILE
