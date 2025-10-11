using System;
using GameResources;
using UnityEngine;

namespace TetrisFields.Items
{
	[Serializable]
	public struct ResourceCellInfo
	{
		public Vector2Int Pos;
		public ResourceType Type;
	}
}