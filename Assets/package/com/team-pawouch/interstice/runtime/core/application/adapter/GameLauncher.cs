// (c)2024-2026 Team Pawouch
// GameLauncher.cs - 2026-11-24 02:11:46

#nullable enable
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Package.Com.TeamPawouch.Interstice.Runtime.Core.Application.Adapter;

public sealed class GameLauncher {
	private static readonly HttpClient _client = new() { Timeout = TimeSpan.FromSeconds(5) };
	private readonly        string[]   _args;
	private readonly        Version    _version;

	internal GameLauncher(string[] args) {
		_args = args;

		_version = Version.TryParse(UnityEngine.Application.version, out Version v) ? v : new Version(0, 1, 0);
	}

	internal async Task Launch() {
		bool update = await CheckUpdate();
		Update(update);
	}

	private async Task<bool> CheckUpdate() {
		if(_args.Contains("-noupdate")) return false;

		return await CanReachInternetAsync();
	}

	public static async Task<bool> CanReachInternetAsync() {
		try {
			HttpResponseMessage resp = await _client.GetAsync("https://www.baidu.com");
			return resp.IsSuccessStatusCode;
		}
		catch {
			return false;
		}
	}

	private void Update(bool update) {
		const string api = "https://api.github.com";
	}
}

// END FILE
