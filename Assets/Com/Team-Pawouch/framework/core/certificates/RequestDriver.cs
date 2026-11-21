// (c)2024-2026 Team Pawouch
// RequestDriver.cs - 2026-11-21 22:11:35

#nullable enable
using System.Collections;
using com.team_pawouch.framework.core.certificates.api;
using com.team_pawouch.framework.core.datas.api;

namespace com.team_pawouch.framework.core.certificates;

public sealed class RequestDriver(IRequestDispatcher dispatcher) : IRequestDriver {
	public IEnumerator? Pump(IQueueKey key, IData parameters) {
		IQueueToken? token = dispatcher.PullDequeue(key);
		return token is null? null : Run(token, parameters);
	}

	private static IEnumerator Run(IQueueToken token, IData parameters) {
		IExecutable? exe;
		try {
			exe = token.Handler.Handle(token.Request, WriterShim(token), token.Cancellation);
		}
		catch(OperationCanceledException) {
			token.Report(RequestStatus.Cancelled, null);
			yield break;
		}
		catch(Exception ex) {
			token.Report(RequestStatus.Failed, ex.Message);
			yield break;
		}

		if(exe is null) {
			token.Report(RequestStatus.Succeeded, null);
			yield break;
		}

		IEnumerator body;

		try {
			body = exe.Execute()(parameters, token.Cancellation) ?? NullEnum();
		}
		catch(OperationCanceledException) {
			token.Report(RequestStatus.Cancelled, null);
			yield break;
		}
		catch(Exception ex) {
			token.Report(RequestStatus.Failed, ex.Message);
			yield break;
		}

		while(true) {
			object? crt;

			try {
				if(!body.MoveNext()) break;
				crt = body.Current;
			}
			catch(OperationCanceledException) {
				token.Report(RequestStatus.Cancelled, null);
				yield break;
			}
			catch(Exception ex) {
				token.Report(RequestStatus.Failed, ex.Message);
				yield break;
			}

			yield return crt;
		}

		token.Report(RequestStatus.Succeeded, null);
	}


	private static IRequestStateWriter WriterShim(IQueueToken token) => new TokenWriter(token);

	private static IEnumerator NullEnum() {
		yield break;
	}

	private sealed class TokenWriter(IQueueToken token) : IRequestStateWriter {
		public IRequestState MarkEnqueued(Guid id, string subject) => new NOOP();
		public IRequestState MarkSolving(Guid  id, string subject) => new NOOP();

		public IRequestState MarkSucceeded(Guid id, string subject) {
			token.Report(RequestStatus.Succeeded, null);
			return new NOOP();
		}

		public IRequestState MarkFailed(Guid id, string subject, string message) {
			token.Report(RequestStatus.Failed, message);
			return new NOOP();
		}

		public IRequestState MarkCanceled(Guid id, string subject) {
			token.Report(RequestStatus.Cancelled, null);
			return new NOOP();
		}
	}

	private sealed record NOOP : IRequestState {
		public Guid           GUID           => Guid.Empty;
		public RequestStatus  Status         => RequestStatus.Generated;
		public string?        FailureMessage => null;
		public DateTimeOffset Updated        => default;
	}
}
