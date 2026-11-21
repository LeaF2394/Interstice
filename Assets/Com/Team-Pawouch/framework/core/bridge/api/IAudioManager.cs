// (c)2024-2026 Team Pawouch
// IAudioManager.cs - 2026-11-21 14:11:33

#nullable enable

using com.team_pawouch.framework.core.certificates;
using com.team_pawouch.framework.core.certificates.api;

namespace com.team_pawouch.framework.core.bridge.api;

public interface IAudioManager : ISingleton<IAudioManager> {
	// IAudioRequest RequestPlay(string           subject, string clip, RequestPayload<AudioClip> payload, int     loopTimes = 1, float volume    = 1f);
	// IAudioRequest RequestPlay3D(string         subject, string clip, RequestPayload<AudioClip> payload, Vector3 coord,         int   loopTimes = 1, float volume = 1f);
	// IAudioRequest RequestPlayBin(string        subject, byte[] data, RequestPayload<AudioClip> payload, int     loopTimes = 1, float volume    = 1.0f);
	// IAudioRequest RequestPlayBin3D(string      subject, byte[] data, RequestPayload<AudioClip> payload, Vector3 coord,         int   loopTimes = 1, float volume = 1.0f);
	// IAudioRequest RequestPlayExternal(string   subject, Uri    file, RequestPayload<AudioClip> payload, int     loopTimes = 1, float volume    = 1.0f);
	// IAudioRequest RequestPlayExternal3D(string subject, Uri    file, RequestPayload<AudioClip> payload, Vector3 coord,         int   loopTimes = 1, float volume = 1.0f);
	// 
	// IStopRequest   RequestStop(string   subject, Guid request, RequestPayload payload);
	// ICancelRequest RequestCancel(string subject, Guid request, RequestPayload payload);
	// 
	// IRequest FetchListener(string subject, RequestPayload<AudioListener> payload);
}

// END FILE
