// (c)2024-2026 Team Pawouch
// UnityLogger.cs - 2026-11-21 11:11:52

#nullable enable
using System.Collections.Generic;
using System.Linq;
using com.team_pawouch.framework.core.runtime.api;
using JetBrains.Annotations;
using ILogger = com.team_pawouch.framework.core.runtime.api.ILogger;

namespace com.team_pawouch.framework.core.runtime;

public sealed class UnityLogger : ILogger {
	public static ILogger ClassInstance {
		get {
			_instance ??= new UnityLogger();
			return _instance;
		}
	}

	private static ILogger? _instance;

	internal class LogRenderer : ILogRenderer {
		[HideInCallstack]
		public void Render(ILogEntry entry) {
			Debug.unityLogger.Log(entry.Severity switch {
				LogSeverity.Info      => LogType.Log,
				LogSeverity.Warning   => LogType.Warning,
				LogSeverity.Error     => LogType.Error,
				LogSeverity.Assert    => LogType.Assert,
				LogSeverity.Exception => LogType.Exception,
				_                     => LogType.Log
			}, entry.ToString());
		}
	}

	internal class LogHistory : ILogHistory {
		private readonly Dictionary<string, LinkedList<ILogEntry>> _log_entries = new();

		public void Add(ILogEntry entry) {
			string key                                           = entry.Subject.ToLowerInvariant();
			if(!_log_entries.ContainsKey(key)) _log_entries[key] = [];
			if(_log_entries[key].Count >= 8_192) _log_entries[key].RemoveFirst();
			_log_entries[key].AddLast(entry);
		}

		public ILogEntry[]? Lookup(string subject) {
			if(!_log_entries.TryGetValue(subject.ToLowerInvariant(), out LinkedList<ILogEntry>? entries)) {
				return null;
			}

			return [.. entries];
		}

		public ILogEntry[]? Lookup(string subject, DateTime until) => Lookup(subject)?.Where(e => e.Timestamp <= until).ToArray();
	}

	public ILogRenderer Renderer { get; set; } = new LogRenderer();
	public ILogHistory  History  { get; set; } = new LogHistory();


	[HideInCallstack]
	public void Log(ILogEntry entry) {
		History.Add(entry);
		Renderer.Render(entry);
	}

	[HideInCallstack]
	public void Assert(IAssertionEntry entry) {
		History.Add(entry);
		if(entry.Condition) return;
		Renderer.Render(entry);
	}

	[StringFormatMethod("format"), HideInCallstack]
	public ILogEntry LogF(LogSeverity severity, string subject, string format, params object[] args) {
		ILogEntry entry = new LogEntry(DateTime.UtcNow, severity, subject, string.Format(format, args));

		Log(entry);

		return entry;
	}

	[StringFormatMethod("format"), HideInCallstack]
	public IAssertionEntry AssertF(bool condition, string subject, string format, params object[] args) {
		IAssertionEntry entry = new AssertionEntry(DateTime.UtcNow, subject, string.Format(format, args), condition);

		Assert(entry);

		return entry;
	}
}

// END FILE
