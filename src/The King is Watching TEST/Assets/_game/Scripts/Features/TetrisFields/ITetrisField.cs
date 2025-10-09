using TetrisFields.Items;
using UnityEngine;

namespace TetrisFields
{
	public interface ITetrisField
	{
		bool CanPutItem(Vector2Int pos, IItem item);
		void PutItem(IItem item, Vector2Int pos);
		void ExtractItem(IItem item);
	}
}