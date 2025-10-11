using UnityEngine;

namespace TetrisFields.Items
{
	public interface IItem
	{
		Vector2Int[] Cells { get; set; }
		ResourceCellInfo ResourceCell { get; }
		void ReplaceTo(Vector2 pos);
		void Capture();
		void Uncapture();
		void Rotate();
		void Destroy();
	}
}