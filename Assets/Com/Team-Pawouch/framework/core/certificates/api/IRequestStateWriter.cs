// (c)2024-2026 Team Pawouch
// IRequestStateWriter.cs - 2026-11-21 20:11:02

#nullable enable
namespace com.team_pawouch.framework.core.certificates.api;

public interface IRequestStateWriter {
	IRequestState MarkEnqueued(Guid  id,        string subject);
	IRequestState MarkSolving(Guid   id, string subject);
	IRequestState MarkSucceeded(Guid id, string subject);
	IRequestState MarkFailed(Guid    id, string subject, string message);
	IRequestState MarkCanceled(Guid  id, string subject);
}
