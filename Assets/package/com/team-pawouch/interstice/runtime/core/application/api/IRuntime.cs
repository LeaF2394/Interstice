// (c)2024-2026 Team Pawouch
// IRuntime.cs - 2026-11-23 02:11:33

#nullable enable
using Package.Com.TeamPawouch.Interstice.Runtime.Core.Api;

namespace Package.Com.TeamPawouch.Interstice.Runtime.Core.Application.Api;

public interface IRuntime : IIdentifier {
	RuntimeState State    { get; }
	int          TickRate { get; set; }
	int          Tick     { get; }
	float        Interval { get; }

	const int DEFAULT_TICK_RATE = 60;

	event Action<int>? OnTickRateChanged, OnTick;
	event Action?      OnTickStarted,     OnTickStopped, OnTickUnpaused;

	void Initialize();
	void Dispose(bool     destroy             = true);
	void StartProcess(int tickRate            = DEFAULT_TICK_RATE);
	void StopProcess(bool keepAccumulatedTick = true);
}

// END FILE
