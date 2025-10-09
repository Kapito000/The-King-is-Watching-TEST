using UnityEngine;

namespace Map
{
	public static class GridExtension
	{
		public static bool HasCell<T>(this IGrid<T> grid, Vector2Int pos) =>
			grid.HasCell(pos.x, pos.y);

		public static bool TrySet<T>(this IGrid<T> grid, T value, Vector2Int pos) =>
			grid.TrySet(value, pos.x, pos.y);

		public static bool TryGet<T>(this IGrid<T> grid, Vector2Int pos,
			out T value) =>
			grid.TryGet(pos.x, pos.y, out value);
	}
}