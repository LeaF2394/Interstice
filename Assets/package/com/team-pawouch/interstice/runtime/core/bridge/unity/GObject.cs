// (c)2024-2026 Team Pawouch
// GObject.cs - 2026-11-23 03:11:33

#nullable enable
using Package.Com.TeamPawouch.Interstice.Runtime.Core.Api;

namespace Package.Com.TeamPawouch.Interstice.Runtime.Core.Bridge.Unity;

public abstract class GObject : MonoBehaviour, IIdentifier {
	public Guid GUID       { get; private set; }
	public int  InstanceId { get; private set; }

	private void Awake() {
		IdentityManager.ClassInstance.Request(this, out Guid guid, out int instance);
		GUID       = guid;
		InstanceId = instance;
	}

	protected virtual void OnDestroy() {
		IdentityManager.ClassInstance.Unregister(this);
		GUID       = Guid.Empty;
		InstanceId = -1;
	}
}

// END FILE
