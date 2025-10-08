using UnityEngine;

public sealed class ItemCell : MonoBehaviour, IItemCell
{
	[SerializeField] Item _item;
	[SerializeField] SpriteRenderer _renderer;

	public IItem Item => _item;

	public void SetItem(Item item)
	{
		_item = item;
	}

	public void SetLayer(string layer)
	{
		_renderer.sortingLayerName = layer;
	}
}