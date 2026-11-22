// (c)2024-2026 Team Pawouch
// IdentityManager.cs - 2026-11-23 02:11:24

#nullable enable
using System.Collections.Generic;
using Package.Com.TeamPawouch.Interstice.Runtime.Core.Api;

namespace Package.Com.TeamPawouch.Interstice.Runtime.Core;

public sealed class IdentityManager {
	public static IdentityManager ClassInstance {
		get {
			_instance ??= new IdentityManager();
			return _instance;
		}
	}

	private static IdentityManager? _instance;

	private int _instanceCount;

	public           IReadOnlyDictionary<Guid, IIdentifier> Registration => _identifiers;
	private readonly Dictionary<Guid, IIdentifier>          _identifiers = new();

	public void Request(IIdentifier identifier, out Guid id, out int instanceId) {
		if(_identifiers.ContainsValue(identifier)) throw new InvalidOperationException();

		id         = Guid.NewGuid();
		instanceId = ++_instanceCount;
		_identifiers.Add(id, identifier);
	}

	public void Unregister(IIdentifier identifier) {
		_identifiers.Remove(identifier.GUID);
	}
}

// END FILE
