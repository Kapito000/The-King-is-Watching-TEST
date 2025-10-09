using Map;
using UnityEngine;

namespace TetrisField
{
	public sealed class FieldGrid : Grid<IFieldCell>
	{
		public FieldGrid(int xSize, int ySize) : base(xSize, ySize)
		{ }

		public bool ContainsItem(int x, int y)
		{
			if (HasCell(x, y) == false)
				return false;

			return _cells[x, y].HasItem;
		}

		public bool ContainsItem(Vector2Int pos) =>
			ContainsItem(pos.x, pos.y);
	}
}