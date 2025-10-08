using UnityEngine;

public sealed class ItemCell : MonoBehaviour, IItemCell
{
	[SerializeField] Item _item;

	public IItem Item => _item;

	public void SetItem(Item item)
	{
		_item = item;
	}
}