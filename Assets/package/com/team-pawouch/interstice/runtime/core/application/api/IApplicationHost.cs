// (c)2024-2026 Team Pawouch
// IApplicationHost.cs - 2026-11-23 02:11:39

#nullable enable
using System.Collections.Generic;

namespace Package.Com.TeamPawouch.Interstice.Runtime.Core.Application.Api;

public interface IApplicationHost {
	int TotalShutdownRequests       { get; }
	int TotalCancelShutdownRequests { get; }

	int   PendingShutdownRequests { get; }
	float ShutdownRemainingTimer  { get; }

	IReadOnlyDictionary<Guid, IRuntime> RuntimeInstances { get; }

	event Action?            OnClientStarted;
	event Action?            OnClientStopped;
	event Action<Exception>? OnClientError;
	event Action?            OnClientCrashed;
	event Action<IRuntime>?  OnRuntimeAttached;
	event Action<IRuntime>?  OnRuntimeDetached;

	void Installation();
	void Uninstallation();

	void AttachRuntime(IRuntime runtime);
	void DetachRuntime(IRuntime runtime);

	void RequestShutdown(float timer, Action onCancelled);
	void CancelShutdown();
}

// END FILE
