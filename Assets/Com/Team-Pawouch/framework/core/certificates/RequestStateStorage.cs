// (c)2024-2026 Team Pawouch
// RequestStateStorage.cs - 2026-11-21 22:11:13

#nullable enable
using System.Collections.Generic;
using com.team_pawouch.framework.core.certificates.api;

namespace com.team_pawouch.framework.core.certificates;

public sealed class RequestStateStorage(IRequestRecorder? recorder = null) : IRequestStateStorage {
	private readonly Dictionary<Guid, RequestStateSnapshot> _states = new();
	private readonly object                                 _gate   = new();

	public IRequestState? TryGet(Guid id) {
		lock(_gate) return _states.GetValueOrDefault(id);
	}

	public IRequestStateWriter Construct() => new Writer(this);

	private RequestStateSnapshot Mutate(Guid id, string subject, RequestStatus status, string? message) {
		lock(_gate) {
			RequestStateSnapshot? prev = _states.GetValueOrDefault(id);
			RequestStateSnapshot next = ( prev ?? new RequestStateSnapshot(id, subject, status, message, DateTimeOffset.UtcNow) )
				.With(status, message);
			_states[id] = next;
			recorder?.Record(next);
			return next;
		}
	}

	private sealed class Writer(RequestStateStorage owner) : IRequestStateWriter {
		public IRequestState MarkEnqueued(Guid  id, string subject) => owner.Mutate(id, subject, RequestStatus.Enqueued,  null);
		public IRequestState MarkSolving(Guid   id, string subject) => owner.Mutate(id, subject, RequestStatus.Solving,   null);
		public IRequestState MarkSucceeded(Guid id, string subject) => owner.Mutate(id, subject, RequestStatus.Succeeded, null);

		public IRequestState MarkFailed(Guid   id, string subject, string message) => owner.Mutate(id, subject, RequestStatus.Failed, message);
		public IRequestState MarkCanceled(Guid id, string subject) => owner.Mutate(id, subject, RequestStatus.Cancelled, null);
	}
}
