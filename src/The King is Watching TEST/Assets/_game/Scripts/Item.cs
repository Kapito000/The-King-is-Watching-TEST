using Extensions;
using UnityEngine;

public sealed class Item : MonoBehaviour, IItem
{
	[SerializeField] Vector2Int[] _cells;

	[field: SerializeField] public ItemOrientation Orientation { get; set; }

	public Vector2Int[] Cells
	{
		get => _cells;
		set => _cells = value.Clone() as Vector2Int[];
	}

	public Vector2Int Pos => transform.position.AsVector2Int();
}