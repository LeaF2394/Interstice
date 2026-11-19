// (c)2024-2026 Team Pawouch
// IResult.cs - 2026-11-20 01:11:32

#nullable enable
using com.team_pawouch.framework.core.certificates.api;

namespace com.team_pawouch.framework.core.datas.api;

public interface IResult {
	IRequest Session { get; }

	IRequestRecord? GetRequest() => CertificatesAPI.RequestRecorder?.GetRequest(Session);
	object?         GetResult()  => CertificatesAPI.RequestRecorder?.GetRequest(Session)?.State.Response;
}

// END FILE
