using System;
using System.Linq;
using System.Reflection;

namespace Indy.IL2CPU {
	public static class ObjectUtilities {
		public static bool IsDelegate(Type aType) {
			if (aType.FullName == "System.Object") {
				return false;
			}
			if (aType.BaseType.FullName == "System.Delegate") {
				return true;
			}
			if (aType.BaseType.FullName == "System.Object") {
				return false;
			}
			return IsDelegate(aType.BaseType);
		}

		public static bool IsArray(Type aType) {
			if (aType.FullName == "System.Object") {
				return false;
			}
			if (aType.BaseType.FullName == "System.Array") {
				return true;
			}
			if (aType.BaseType.FullName == "System.Object") {
				return false;
			}
			return IsArray(aType.BaseType);
		}

		public static int GetObjectStorageSize(Type aType) {
			if (aType == null) {
				throw new ArgumentNullException("aType");
			}
			int xResult = ObjectImpl.FieldDataOffset;
			if (IsDelegate(aType)) {
				xResult += 8;
			}
			foreach (FieldInfo xField in aType.GetFields(BindingFlags.NonPublic | BindingFlags.Public)) {
				if (xField.IsStatic) {
					continue;
				}
				if (!xField.FieldType.IsValueType) {
					xResult += 4;
				} else {
					xResult += Engine.GetFieldStorageSize(xField.FieldType);
				}
			}
			return xResult;
		}
	}
}