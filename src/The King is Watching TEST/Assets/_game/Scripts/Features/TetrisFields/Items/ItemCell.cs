using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace TetrisFields.Items
{
	public class ItemCell : MonoBehaviour, IItemCell
	{
		[SerializeField] SpriteRenderer _renderer;
		
		Subject<string> _renderLayerChanged = new();
		public IObservable<string> RenderLayerChanged => _renderLayerChanged;

		void Awake()
		{
			Assert.IsNotNull(_renderer);
		}

		public void SetRenderLayer(string layer)
		{
			_renderer.sortingLayerName = layer;
			_renderLayerChanged.OnNext(layer);
		}
	}
}