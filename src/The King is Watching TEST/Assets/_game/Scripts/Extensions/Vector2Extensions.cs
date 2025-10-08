using UnityEngine;

namespace Extensions
{
	public static class Vector2Extensions
	{
		public static Vector3 AsVector3(this Vector2Int v) =>
			new(v.x, v.y, 0);

		public static Vector2Int Rotate90(this Vector2Int v) =>
			new(v.y, -v.x);

		public static Vector2Int Rotate180(this Vector2Int v) =>
			-v;

		public static Vector2Int Rotate270(this Vector2Int v) =>
			new(-v.y, v.x);
	}
}