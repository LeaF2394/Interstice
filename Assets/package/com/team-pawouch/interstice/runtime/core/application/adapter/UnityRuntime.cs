// (c)2024-2026 Team Pawouch
// UnityRuntime.cs - 2026-11-22 15:11:13

#nullable enable
using System;
using System.Collections;
using UnityEngine;

namespace Package.Com.TeamPawouch.Interstice.Runtime.Core.Application.Adapter;

public enum RuntimeState {
	None,
	Runtime,
	Stopped,
}

internal class UnityRuntime : MonoBehaviour {
	internal static UnityRuntime ClassInstance {
		get {
			if(_instance == null) {
				GameObject go = new(Environment.ProcessorCount.ToString()) {
					hideFlags = HideFlags.NotEditable
				};

				_instance = go.AddComponent<UnityRuntime>();
			}

			return _instance;
		}
	}

	private static UnityRuntime? _instance;

	internal int TickRate {
		get => _tickRate;
		set {
			if(value <= 0) {
				_tickRate = DEFAULT_TICK_RATE;
				throw new ArgumentException("Tick rate must be greater than zero");
			}

			if(_tickRate == value) return;

			_tickRate = value;
			OnTickRateChanged?.Invoke(value);
		}
	}

	internal int Tick {
		get => _tick;
		private set {
			if(_tick == value) return;

			_tick = value;
			OnTick?.Invoke(value);
		}
	}

	private int _tickRate;
	private int _tick;

	internal event Action<int>? OnTickRateChanged;
	internal event Action<int>? OnTick;

	internal event Action? OnTickStarted, OnTickStopped;

	private const int DEFAULT_TICK_RATE = 60;

	internal void Installation() { }

	internal void Uninstallation() { }

	internal void StartProcess(int tickRate = DEFAULT_TICK_RATE) { }

	internal void StopProcess(bool keepAccumulatedTick = true) {
		if(!keepAccumulatedTick) _tick = 0;

		OnTickStopped?.Invoke();
	}

	private IEnumerator Process(int tickRate) {
		if(tickRate == 0) throw new ArgumentException(nameof(tickRate));

		TickRate = tickRate;
		float interval = 1f / TickRate;
		float elapsed  = 0f;

		OnTickRateChanged -= UpdateInterval;
		OnTickRateChanged += UpdateInterval;

		OnTickStarted?.Invoke();
		while(true) {
			if(!enabled) {
				yield return null;
				continue;
			}

			if(!gameObject) break;

			yield return null;
			elapsed += Time.deltaTime;

			if(elapsed < interval) continue;

			Tick++;
			elapsed -= interval;
		}

		// Destroy
		OnTickRateChanged -= UpdateInterval;
		yield break;

		void UpdateInterval(int tr) => interval = 1f / tr;
	}
}

// END FILE
