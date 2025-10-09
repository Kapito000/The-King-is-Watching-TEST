using UnityEngine;
using Menu = Constant.CreateAssetMenu.MenuName;

namespace TetrisFields.Items.StaticData
{
	[CreateAssetMenu(menuName = Menu.StaticData + nameof(ItemData))]
	public sealed class ItemData : ScriptableObject, IItemData
	{
		[field: SerializeField] public Vector2Int[] Cells { get; private set; }
	}
}