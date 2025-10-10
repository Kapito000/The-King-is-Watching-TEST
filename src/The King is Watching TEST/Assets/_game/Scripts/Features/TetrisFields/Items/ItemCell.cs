using UnityEngine;
using UnityEngine.Assertions;

namespace TetrisFields.Items
{
	public sealed class ItemCell : MonoBehaviour, IItemCell
	{
		[SerializeField] SpriteRenderer _renderer;

		void Awake()
		{
			Assert.IsNotNull(_renderer);
		}

		public void SetRenderLayer(string layer)
		{
			_renderer.sortingLayerName = layer;
		}
	}
}