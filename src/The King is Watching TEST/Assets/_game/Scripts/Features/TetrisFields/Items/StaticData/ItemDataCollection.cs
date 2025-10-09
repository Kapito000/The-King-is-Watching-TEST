using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menu = Constant.CreateAssetMenu.MenuName;

namespace TetrisFields.Items.StaticData
{
	[CreateAssetMenu(menuName = Menu.StaticData + nameof(ItemDataCollection))]
	public sealed class ItemDataCollection : ScriptableObject, IItemDataCollection
	{
		[SerializeField] ItemData[] _items;

		public IEnumerator<IItemData> GetEnumerator()
		{
			foreach (var item in _items)
				yield return item;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}