using TetrisFields.Items;
using UnityEngine;

namespace TetrisFields
{
	public sealed class FieldCell : MonoBehaviour, IFieldCell
	{
		IItem _item;
		public IItem Item => _item;

		[field: SerializeField] public bool HasItem { get; private set; }
		[field: SerializeField] public Vector2Int FieldPos { get; set; }

		public Vector2 Pos
		{
			get => transform.position;
			set => transform.position = new Vector3(value.x, value.y, 0);
		}

		public void PlaceItem(IItem item)
		{
			_item = item;
			HasItem = true;
		}

		public IItem ExtractItem()
		{
			_item	= null;
			HasItem = false;
			return _item;
		}
	}
}