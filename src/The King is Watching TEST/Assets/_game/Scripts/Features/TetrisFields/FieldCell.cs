using UnityEngine;

namespace TetrisFields
{
	public sealed class FieldCell : MonoBehaviour, IFieldCell
	{
		[field: SerializeField] public Vector2Int FieldPos { get; set; }

		public Vector2 Pos
		{
			get => transform.position;
			set => transform.position = new Vector3(value.x, value.y, 0);
		}
	}
}