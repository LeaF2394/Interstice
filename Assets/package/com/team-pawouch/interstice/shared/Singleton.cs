// (c)2024-2026 Team Pawouch
// Singleton.cs - 2026-11-24 00:11:16

#nullable enable
using System;

namespace Package.Com.TeamPawouch.Interstice.Shared {
	public static class Singleton<T> {
		public static T? ClassInstance { get; private set; }

		public static void CreateInstance(Func<T> creator) {
			if(ClassInstance != null) throw new InvalidOperationException();

			ClassInstance = creator();
		}

		public static void DestroyInstance() {
			ClassInstance = default;
		}
	}
}

// END FILE
