using UnityEngine;

public sealed class FieldCell : MonoBehaviour, IFieldCell
{
	[field: SerializeField] public Vector2Int Pos { get; set; }
}