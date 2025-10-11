using System;
using UnityEngine;

namespace ProductionCells.StaticData
{
	[Serializable]
	public struct ProductionCellData
	{
		public Color Color;
		[Range(0, 1)]
		public float ProductionModifier;
		public float ProductionTimer;
	}
}