// (c)2024-2026 Team Pawouch
// IIdentifier.cs - 2026-11-23 02:11:02

#nullable enable
namespace Package.Com.TeamPawouch.Interstice.Runtime.Core.Api;

public interface IIdentifier {
	Guid GUID       { get; }
	int  InstanceId { get; }
}

// END FILE
