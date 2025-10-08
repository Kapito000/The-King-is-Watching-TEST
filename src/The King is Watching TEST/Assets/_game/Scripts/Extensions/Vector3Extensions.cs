using UnityEngine;

namespace Extensions
{
	public static class Vector3Extensions
	{
		public static Vector2Int AsVector2Int(this Vector3 v) =>
			new Vector2Int((int)v.x, (int)v.y);
	}
}