// (c)2024-2026 Team Pawouch
// ILogger.cs - 2026-11-21 10:11:45

#nullable enable
using JetBrains.Annotations;

namespace com.team_pawouch.framework.core.runtime.api;

public interface ILogger {
	ILogRenderer Renderer { get; set; }
	ILogHistory  History  { get; set; }

	[HideInCallstack] void Log(ILogEntry entry);

	[HideInCallstack] void Assert(IAssertionEntry entry);

	[StringFormatMethod("format"), HideInCallstack]
	ILogEntry LogF(LogSeverity severity, string subject, string format, params object[] args);

	[StringFormatMethod("format"), HideInCallstack]
	ILogEntry InfoF(string subject, string format, params object[] args) => LogF(LogSeverity.Info, subject, format, args);

	[StringFormatMethod("format"), HideInCallstack]
	ILogEntry WarnF(string subject, string format, params object[] args) => LogF(LogSeverity.Warning, subject, format, args);

	[StringFormatMethod("format"), HideInCallstack]
	ILogEntry ErrorF(string subject, string format, params object[] args) => LogF(LogSeverity.Error, subject, format, args);

	[StringFormatMethod("format"), HideInCallstack]
	ILogEntry ExceptF(string subject, string format, params object[] args) => LogF(LogSeverity.Exception, subject, format, args);

	[StringFormatMethod("format"), HideInCallstack]
	IAssertionEntry AssertF(bool condition, string subject, string format, params object[] args);
}

// END FILE
