// (c)2024-2026 Team Pawouch
// Bootstrap.cs - 2026-11-22 15:11:03

#nullable enable
using System;
using System.IO;
using Package.Com.TeamPawouch.Interstice.Runtime.Core.Application.Adapter;
using UnityEngine;

namespace Package.Com.TeamPawouch.Interstice.Runtime.Core.Application;

internal static class Bootstrap {
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void Complete() {
		string gameRoot = $".{UnityEngine.Application.productName.ToLower()}";
		string rootPath = AppDomain.CurrentDomain.BaseDirectory;

		Directory.CreateDirectory(Path.Combine(rootPath, gameRoot));
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	private static void Main() {
		string[] args = Environment.GetCommandLineArgs();
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Adapt() {
		UnityRuntime.ClassInstance.Installation();
		UnityRuntime.ClassInstance.StartProcess();
	}
}

// END FILE
