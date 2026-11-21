// (c)2024-2026 Team Pawouch
// Bootloader.cs - 2026-11-21 09:11:42

#nullable enable
using com.team_pawouch.framework.core.runtime;

namespace com.team_pawouch.framework.core.applications;

internal static class Bootloader {
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	private static void boot() {
		_ = UnityLogger.ClassInstance.InfoF("Bootloader", "Prepare for unity host.");
	}
}

// END FILE
