using TetrisFields.Items;
using UnityEngine;

namespace TetrisFields
{
	public interface ITetrisField
	{
		bool CanPutItem(IItem item, Vector2Int pos);
		void PutItem(IItem item, Vector2Int pos);
		void ExtractItem(IItem item);
		bool HasItemAt(Vector2Int pos);
		IItem GetItemAt(Vector2Int pos);
	}
}