// (c)2024-2026 Team Pawouch
// UnityRuntime.cs - 2026-11-22 15:11:13

#nullable enable
using System.Collections;
using Package.Com.TeamPawouch.Interstice.Runtime.Core.Application.Api;
using Package.Com.TeamPawouch.Interstice.Runtime.Core.Bridge.Unity;

namespace Package.Com.TeamPawouch.Interstice.Runtime.Core.Application.Adapter;

internal sealed class UnityRuntime : GObject, IRuntime {
	public static IRuntime? ClassInstance { get; private set; }

	public RuntimeState State { get; private set; }

	public int TickRate {
		get => _tickRate;
		set {
			if(value <= 0) throw new ArgumentException("Tick rate must be greater than zero");

			if(_tickRate == value) return;

			_tickRate = value;
			OnTickRateChanged?.Invoke(value);
		}
	}

	public int Tick {
		get => _tick;
		private set {
			if(_tick == value) return;

			if(State != RuntimeState.Runtime) return;

			_tick = value;
			OnTick?.Invoke(value);
		}
	}

	public float Interval => 1f / TickRate;

	private int _tickRate;
	private int _tick;

	public event Action<int>? OnTickRateChanged, OnTick;
	public event Action?      OnTickStarted,     OnTickStopped, OnTickUnpaused;

	private Coroutine? _process;

	public static IRuntime CreateInstance() {
		GameObject go = new($"{typeof(UnityRuntime).FullName}") {
			hideFlags = HideFlags.NotEditable
		};

		DontDestroyOnLoad(go);

		return go.AddComponent<UnityRuntime>();
	}

	public void Initialize() {
		if(State != RuntimeState.None) throw new InvalidOperationException();
		if(ClassInstance != null) {
			Destroy(gameObject);
			throw new InvalidOperationException();
		}

		ClassInstance = this;
		TickRate      = IRuntime.DEFAULT_TICK_RATE;
		_tick         = 0;

		State = RuntimeState.Initialized;
	}

	public void Dispose(bool destroy = true) {
		if(State == RuntimeState.None) return;

		OnTickRateChanged = null;
		OnTick            = null;
		OnTickStarted     = null;
		OnTickStopped     = null;
		OnTickUnpaused    = null;

		if(State == RuntimeState.Runtime) StopProcess(false);
		State = RuntimeState.Destroy;

		if(destroy) Destroy(gameObject);
	}

	public void StartProcess(int tickRate = IRuntime.DEFAULT_TICK_RATE) {
		if(State is RuntimeState.Runtime or RuntimeState.None || _process != null) throw new InvalidOperationException();

		TickRate = tickRate;

		_process = StartCoroutine(Process());
	}

	public void StopProcess(bool keepAccumulatedTick = true) {
		if(State != RuntimeState.Runtime || _process == null) throw new InvalidOperationException();

		if(!keepAccumulatedTick) _tick = 0;

		State = RuntimeState.Stopped;
		StopCoroutine(_process);
		_process = null;
		OnTickStopped?.Invoke();
	}

	private IEnumerator Process() {
		float elapsed = 0f;

		if(State == RuntimeState.Stopped) OnTickUnpaused?.Invoke();
		else OnTickStarted?.Invoke();

		State = RuntimeState.Runtime;
		while(gameObject && State == RuntimeState.Runtime) {
			if(!isActiveAndEnabled) {
				yield return null;
				continue;
			}

			elapsed += Time.deltaTime;

			while(elapsed >= Interval) {
				Tick++;
				elapsed -= Interval;
			}

			yield return null;
		}
	}

	protected override void OnDestroy() {
		if(State != RuntimeState.Destroy) Dispose(false);

		ClassInstance = null;
		State         = RuntimeState.None;
		base.OnDestroy();
	}
}

// END FILE
