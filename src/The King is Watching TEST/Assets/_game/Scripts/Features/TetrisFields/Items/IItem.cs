using UnityEngine;

namespace TetrisFields.Items
{
	public interface IItem
	{
		Vector2Int[] Cells { get; set; }
		void ReplaceTo(Vector2 pos);
		void Capture();
		void Uncapture();
		void Rotate();
	}
}