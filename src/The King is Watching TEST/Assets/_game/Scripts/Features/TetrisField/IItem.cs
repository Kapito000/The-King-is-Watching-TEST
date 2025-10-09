using UnityEngine;

namespace TetrisField
{
	public interface IItem
	{
		void Capture();
		void Uncapture();
		void MoveTo(Vector2 pos);
		void Rotate();
		Vector2Int[] Cells { get; set; }
		void PutToField();
	}
}