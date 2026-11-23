// (c)2024-2026 Team Pawouch
// ILogger.cs - 2026-11-24 01:11:19

#nullable enable
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace Package.Com.TeamPawouch.Interstice.Runtime.Diagnosis.Api {
	public interface ILogger {
		StringBuilder MessageHistory { get; }
		StringBuilder LogFileContent { get; }

		string LogFilePath { get; }

		void Installation(string logFile);
		void Uninstallation();

		[StringFormatMethod("format"), HideInCallstack]
		void Log(LogLevel level, string format, params object[] args);

		void Clear();
	}
}

// END FILE
