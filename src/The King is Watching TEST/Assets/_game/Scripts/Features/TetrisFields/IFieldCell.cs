using TetrisFields.Items;
using UnityEngine;

namespace TetrisFields
{
	public interface IFieldCell
	{
		bool HasItem { get; }
		IItem Item {get; }
		Vector2 Pos { get; set; }
		Vector2Int FieldPos { get; set; }

		void Place(IItem item);
		IItem ExtractItem();
	}
}