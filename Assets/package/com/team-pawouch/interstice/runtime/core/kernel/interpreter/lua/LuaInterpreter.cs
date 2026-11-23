// (c)2024-2026 Team Pawouch
// LuaInterpreter.cs - 2026-11-23 03:11:59

#nullable enable
using System.IO;
using Package.Com.TeamPawouch.Interstice.Runtime.Core.Application;
using Package.Com.TeamPawouch.Interstice.Runtime.Core.Application.Api;
using Package.Com.TeamPawouch.Interstice.Shared;
using XLua;

namespace Package.Com.TeamPawouch.Interstice.Runtime.Core.Kernel.Interpreter.Lua;

public static class LuaInterpreter {
	private static IRuntime? _runtime;
	private static LuaEnv?   _env;

	public static LuaTable? Global => _env?.Global;

	internal static void Installation(IRuntime? runtime) {
		if(_runtime != null || _env != null) throw new InvalidOperationException();

		_runtime = runtime ?? throw new ArgumentNullException();
		_env     = new LuaEnv();

		AppPath? appPath = Singleton<AppPath>.ClassInstance;

		if(appPath == null) {
			throw new InvalidOperationException("Runtime Application Path Not Ready Yet.");
		}

		_env.AddLoader((ref string filepath) => {
			try {
				string path = Path.Combine(Singleton<AppPath>.ClassInstance!.ModPath, filepath + ".lua");
				return !File.Exists(path)? null : File.ReadAllBytes(path);
			}
			catch(Exception ex) {
				_runtime.ReportError(ex);
				return null;
			}
		});

		_runtime.OnTick -= OnTick;
		_runtime.OnTick += OnTick;
	}

	internal static void Uninstallation() {
		if(_runtime != null) _runtime.OnTick -= OnTick;

		_env?.Dispose();
		_env     = null;
		_runtime = null;
	}

	private static void OnTick(int tick) {
		if(_env == null) return;

		try {
			_env.Tick();
		}
		catch(Exception ex) {
			_runtime?.ReportError(ex);
		}
	}

	public static object[]? InterpretCode(string code) {
		if(_env is null) {
			_runtime?.ReportError(new NullReferenceException());
			return null;
		}

		try {
			return _env.DoString(code);
		}
		catch(Exception ex) {
			_runtime?.ReportError(ex);
			return null;
		}
	}

	public static object[]? Interpret(string file) {
		string code = $"require '{file}'";
		return InterpretCode(code);
	}
}

// END FILE
