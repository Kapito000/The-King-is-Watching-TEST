using UnityEngine;

public interface IItem
{
	Vector2Int Pos { get; }
	Vector2Int[] Cells { get; set; }
	ItemOrientation Orientation { get; set; }
}