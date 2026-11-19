// (c)2024-2026 Team Pawouch
// IRequestRecord.cs - 2026-11-20 02:11:55

#nullable enable
namespace com.team_pawouch.framework.core.certificates.api;

public interface IRequestRecord {
	DateTime      Time    { get; }
	IRequest      Session { get; }
	IRequestState State   { get; }
	string        Name    { get; }
	string        Summary { get; }
}

// END FILE