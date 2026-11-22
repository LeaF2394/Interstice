// (c)2024-2026 Team Pawouch
// BatchCopyWindow.cs - 2026-11-22 14:11:44

#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor;

public class BatchCopyWindow : EditorWindow {
	private readonly List<string> sourceFiles = [];
	private readonly List<string> targetDirs  = [];

	private Vector2 scrollPos;
	private bool    overwriteExisting = true;

	[MenuItem("Team Pawouch/Tools/Batch Copy Window")]
	public static void Open() {
		BatchCopyWindow win = GetWindow<BatchCopyWindow>("Batch Copy");
		win.minSize = new Vector2(480, 320);
		win.Show();
	}

	private void OnGUI() {
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Batch copy files to multiple directories", EditorStyles.boldLabel);
		EditorGUILayout.HelpBox(
			"Line 1: List of files to be copied (you can drag files from the Project panel into the corresponding slots).\n" +
			"Second line: Target directory list (you can drag folders in here).\n"                                           +
			"When executed, it will copy each file to every directory (number of files × number of directories).",
			MessageType.Info);

		EditorGUILayout.Space();

		EditorGUILayout.LabelField("① List of files to be copied", EditorStyles.boldLabel);
		DrawStringList(sourceFiles, "Drag files here", ValidateFile);

		EditorGUILayout.Space();

		EditorGUILayout.LabelField("② Target directory list", EditorStyles.boldLabel);
		DrawStringList(targetDirs, "Drag the folder here", ValidateDir);

		EditorGUILayout.Space();
		overwriteExisting = EditorGUILayout.Toggle("Overwrite existing file", overwriteExisting);

		EditorGUILayout.Space();

		using(new EditorGUI.DisabledScope(sourceFiles.Count == 0 || targetDirs.Count == 0)) {
			if(GUILayout.Button($"Start copying({sourceFiles.Count} Files × {targetDirs.Count} Directories)",
			                    GUILayout.Height(32))) {
				DoCopy();
			}
		}

		if(GUILayout.Button("Clear list")) {
			sourceFiles.Clear();
			targetDirs.Clear();
		}

		EditorGUILayout.EndScrollView();
	}

	private static void DrawStringList(List<string> list, string dragHint, Func<string, bool> validator) {
		Rect dropRect = GUILayoutUtility.GetRect(0, 22, GUILayout.ExpandWidth(true));
		GUI.Box(dropRect, dragHint, EditorStyles.helpBox);
		HandleDragDrop(dropRect, list, validator);

		for(int i = 0;i < list.Count;i++) {
			EditorGUILayout.BeginHorizontal();
			list[i] = EditorGUILayout.TextField(list[i]);

			if(GUILayout.Button("...", GUILayout.Width(28))) {
				string picked = validator == ValidateFile
					                ? EditorUtility.OpenFilePanel("Select file", Application.dataPath, "")
					                : EditorUtility.OpenFolderPanel("Select Directory", Application.dataPath, "");
				if(!string.IsNullOrEmpty(picked)) {
					list[i]     = picked;
					GUI.changed = true;
				}
			}

			if(GUILayout.Button("-", GUILayout.Width(28))) {
				list.RemoveAt(i);
				break;
			}

			EditorGUILayout.EndHorizontal();
		}

		if(GUILayout.Button("+ Add a row", GUILayout.Width(100))) {
			list.Add("");
		}
	}

	private static void HandleDragDrop(Rect dropRect, List<string> list, Func<string, bool>? validator) {
		Event evt = Event.current;
		if(!dropRect.Contains(evt.mousePosition)) return;

		switch(evt.type) {
			case EventType.DragUpdated:
				DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
				evt.Use();
				break;

			case EventType.DragPerform:
				DragAndDrop.AcceptDrag();
				list.AddRange(from obj in DragAndDrop.objectReferences select AssetDatabase.GetAssetPath(obj) into path where !string.IsNullOrEmpty(path) select Path.GetFullPath(path) into full where validator == null || validator(full) select full);

				list.AddRange(DragAndDrop.paths.Where(p => validator == null || validator(p)));

				evt.Use();
				break;
		}
	}

	private static bool ValidateFile(string p) {
		return !string.IsNullOrEmpty(p) && File.Exists(p);
	}

	private static bool ValidateDir(string p) {
		return !string.IsNullOrEmpty(p) && Directory.Exists(p);
	}

	private void DoCopy() {
		int          success = 0, fail = 0;
		List<string> errors  = [];

		foreach(string src in sourceFiles) {
			if(string.IsNullOrEmpty(src) || !File.Exists(src)) {
				fail++;
				errors.Add($"Source file does not exist:{src}");
				continue;
			}

			string fileName = Path.GetFileName(src);

			foreach(string dir in targetDirs) {
				if(string.IsNullOrEmpty(dir)) {
					fail++;
					errors.Add("There are empty target directory entries.");
					continue;
				}

				try {
					if(!Directory.Exists(dir))
						Directory.CreateDirectory(dir);

					string dest = Path.Combine(dir, fileName);

					if(File.Exists(dest)) {
						if(!overwriteExisting) {
							errors.Add($"Skipped (already exists): {dest}");
							continue;
						}

						FileUtil.DeleteFileOrDirectory(dest);
					}

					File.Copy(src, dest, true);
					success++;
					Debug.LogFormat("BatchCopy: {0} -> {1}", src, dest);
				}
				catch(Exception e) {
					fail++;
					errors.Add($"Copy failed: {src} -> {dir} ({e.Message})");
				}
			}
		}

		AssetDatabase.Refresh();

		string msg = $"Batch copying completed! \nSuccess: {success}  Failure: {fail}";
		if(errors.Count > 0)
			msg += "\n\nPlease check the Console for details.";

		EditorUtility.DisplayDialog("Batch Copy", msg, "Yes");

		foreach(string e in errors)
			Debug.LogWarningFormat("BatchCopy: {0}", e);
	}
}

// END FILE
