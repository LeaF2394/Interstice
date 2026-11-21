// (c)2024-2026 Team Pawouch
// QueueKey.cs - 2026-11-21 20:11:04

#nullable enable
using com.team_pawouch.framework.core.certificates.api;

namespace com.team_pawouch.framework.core.certificates;

public sealed record QueueKey(Guid GUID, string Name) : IQueueKey;

// END FILE
