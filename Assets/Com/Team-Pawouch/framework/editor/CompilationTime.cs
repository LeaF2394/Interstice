// (c)2024-2026 Team Pawouch
// CompilationTime.cs - 2026-11-21 12:11:02

#if UNITY_EDITOR

using System.Collections.Generic;
using System.Diagnostics;
using com.team_pawouch.framework.core.runtime;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

#nullable enable
namespace com.team_pawouch.framework.editor;

[InitializeOnLoad]
public static class CompilationTime {
	private static readonly object                        _lk      = new();
	private static readonly Dictionary<object, Stopwatch> _watches = new();

	static CompilationTime() {
		lock(_lk) {
			CompilationPipeline.compilationStarted  -= Started;
			CompilationPipeline.compilationStarted  += Started;
			CompilationPipeline.compilationFinished -= Finished;
			CompilationPipeline.compilationFinished += Finished;
		}
	}

	private static void Started(object obj) {
		lock(_lk) {
			_watches[obj] = Stopwatch.StartNew();
			EditorPrefs.SetInt("0", EditorPrefs.GetInt("0", 0) + 1);
		}
	}

	private static void Finished(object obj) {
		lock(_lk) {
			if(_watches.TryGetValue(obj, out Stopwatch? watch)) {
				watch.Stop();
				_ = UnityLogger.ClassInstance.InfoF("Compiler", "Compilation <color=#{0}>#{1}</color> >> <color=#{0}>{2:F2}</color> ms", ColorUtility.ToHtmlStringRGB(Color.lightGoldenRod), EditorPrefs.GetInt("0", 0), watch.Elapsed.TotalMilliseconds);
			}
			else {
				_ = UnityLogger.ClassInstance.WarnF("Compiler", "Compilation Finished. <color=#{0}>(404 NO CONTEXT FOUND)</color>", ColorUtility.ToHtmlStringRGB(Color.crimson));
			}

			_watches.Remove(obj);
		}
	}
}

#endif

// END FILE
