using UnityEngine;

public interface IFieldCell
{
	bool HasItem { get; }
	Vector2 Pos { get; set; }
	Vector2Int FieldPos { get; set; }

	void Place(IItem item);
	IItem ExtractItem();
}