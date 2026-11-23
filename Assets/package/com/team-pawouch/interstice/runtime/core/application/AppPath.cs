// (c)2024-2026 Team Pawouch
// AppPath.cs - 2026-11-24 00:11:54

#nullable enable
using System.IO;

namespace Package.Com.TeamPawouch.Interstice.Runtime.Core.Application;

public class AppPath {
	public string BasePath   { get; set; } = "";
	public string GamePath   { get; set; } = "";
	public string ConfigPath => Path.Combine(GamePath, "cfg");
	public string ModPath    => Path.Combine(GamePath, "mods");
}

// END FILE
