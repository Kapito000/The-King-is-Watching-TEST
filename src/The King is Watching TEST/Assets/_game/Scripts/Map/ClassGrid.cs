using UnityEngine;

namespace Map
{
	public sealed class ClassGrid<T> : Grid<T> where T : class
	{
		public ClassGrid(int xSize, int ySize) : base(xSize, ySize)
		{ }

		public bool ContainsItem(int x, int y)
		{
			if (HasCell(x, y) == false)
				return false;

			return _cells[x, y] != null;
		}

		public bool ContainsItem(Vector2Int pos) =>
			ContainsItem(pos.x, pos.y);
	}
}