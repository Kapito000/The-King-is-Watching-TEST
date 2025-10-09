using UnityEngine;
using UnityEngine.Assertions;

namespace TetrisFields
{
	public sealed class ItemCell : MonoBehaviour, IItemCell
	{
		[SerializeField] Item _item;
		[SerializeField] Collider2D _collider;
		[SerializeField] SpriteRenderer _renderer;

		public IItem Item => _item;

		void Awake()
		{
			Assert.IsNotNull(_collider);
			Assert.IsNotNull(_renderer);
		}

		public void SetItem(Item item)
		{
			_item = item;
		}

		public void SetLayer(string layer)
		{
			_renderer.sortingLayerName = layer;
		}

		public void EnableCollider(bool enable)
		{
			_collider.enabled = enable;
		}
	}
}