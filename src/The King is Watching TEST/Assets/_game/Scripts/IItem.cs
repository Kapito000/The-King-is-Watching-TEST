using UnityEngine;

public interface IItem
{
	Vector2Int[] Cells { get; set; }
	ItemOrientation Orientation { get; set; }
	void AddItemCell(IItemCell itemCell);
	void Capture();
	void Uncapture();
	void MoveTo(Vector2 pos);
}