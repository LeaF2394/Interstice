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
	private static IRuntime?    _runtime;
	private static LuaEnv?      _env;
	private static LuaFunction? _require;
	private static string?      _modPath;

	public static LuaTable? Global => _env?.Global;

	public static bool IsInstalled => _env != null && _runtime != null;

	internal static void Installation(IRuntime? runtime) {
		if(IsInstalled) throw new InvalidOperationException("LuaInterpreter already installed.");
		if(runtime is null) throw new ArgumentNullException(nameof(runtime));

		AppPath? appPath = Singleton<AppPath>.ClassInstance;
		if(appPath is null || string.IsNullOrEmpty(appPath.ModPath))
			throw new InvalidOperationException("Runtime Application Path Not Ready Yet.");

		// 先把所有可能失败的步骤做完，最后一次性提交状态，避免半初始化。
		LuaEnv       env     = new();
		string       modPath = appPath.ModPath;
		LuaFunction? require = null;

		try {
			env.AddLoader((ref string filepath) => LoadFromMod(ref filepath, modPath, runtime));
			require = env.Global.Get<LuaFunction>("require");
		}
		catch {
			RequireGuard.Dispose(ref require);
			env.Dispose();
			throw;
		}

		_runtime = runtime;
		_modPath = modPath;
		_env     = env;
		_require = require;

		runtime.OnTick -= OnTick;
		runtime.OnTick += OnTick;
	}

	internal static void Uninstallation() {
		if(_runtime != null) _runtime.OnTick -= OnTick;

		RequireGuard.Dispose(ref _require);

		try {
			_env?.Dispose();
		}
		catch(Exception ex) {
			_runtime?.ReportError(ex);
		}
		finally {
			_env     = null;
			_runtime = null;
			_modPath = null;
		}
	}

	private static byte[]? LoadFromMod(ref string filepath, string modPath, IRuntime runtime) {
		try {
			string logicalName = filepath.Replace('.', Path.DirectorySeparatorChar);
			string path        = Path.Combine(modPath, logicalName + ".lua");

			if(!File.Exists(path)) return null;

			byte[] bytes = File.ReadAllBytes(path);

			if(bytes is not [0xEF, 0xBB, 0xBF, ..]) return bytes;

			byte[] stripped = new byte[bytes.Length - 3];
			Buffer.BlockCopy(bytes, 3, stripped, 0, stripped.Length);
			return stripped;
		}
		catch(Exception ex) {
			runtime.ReportError(ex);
			return null;
		}
	}

	private static void OnTick(int tick) {
		if(_env is null) return;
		try {
			_env.Tick();
		}
		catch(Exception ex) {
			_runtime?.ReportError(ex);
		}
	}

	public static object[]? InterpretCode(string code) {
		if(string.IsNullOrEmpty(code)) throw new ArgumentException("Code must not be null or empty.", nameof(code));
		if(!EnsureEnv(out LuaEnv env)) return null;

		try {
			return env.DoString(code);
		}
		catch(Exception ex) {
			_runtime!.ReportError(ex);
			return null;
		}
	}

	public static object[]? Interpret(string file) {
		if(string.IsNullOrEmpty(file)) throw new ArgumentException("File must not be null or empty.", nameof(file));
		if(!EnsureEnv(out _)) return null;

		try {
			return _require!.Call(file);
		}
		catch(Exception ex) {
			_runtime!.ReportError(ex);
			return null;
		}
	}

	private static bool EnsureEnv(out LuaEnv env) {
		if(_env != null && _runtime != null && _require != null) {
			env = _env;
			return true;
		}

		env = null!;
		_runtime?.ReportError(new InvalidOperationException("LuaInterpreter is not installed."));
		return false;
	}

	private static class RequireGuard {
		internal static void Dispose(ref LuaFunction? func) {
			try {
				func?.Dispose();
			}
			catch(Exception ex) {
				_runtime?.ReportError(ex);
			}
			finally {
				func = null;
			}
		}
	}
}

// END FILE
