using System.Collections.Generic;

namespace TetrisFields.Items.StaticData
{
	public interface IItemDataCollection : IEnumerable<IItemData>
	{
		ItemData[] Items { get; }
	}
}