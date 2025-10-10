using UnityEngine;

namespace TetrisFields
{
	public interface IFieldCell
	{
		Vector2 Pos { get; set; }
		Vector2Int FieldPos { get; set; }
	}
}