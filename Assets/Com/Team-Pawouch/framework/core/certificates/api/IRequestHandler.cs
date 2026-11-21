// (c)2024-2026 Team Pawouch
// IRequestHandler.cs - 2026-11-21 20:11:21

#nullable enable
using System.Threading;

namespace com.team_pawouch.framework.core.certificates.api;

public interface IRequestHandler {
	IExecutable? Handle(IRequest request, IRequestStateWriter writer, CancellationToken ct);
}
