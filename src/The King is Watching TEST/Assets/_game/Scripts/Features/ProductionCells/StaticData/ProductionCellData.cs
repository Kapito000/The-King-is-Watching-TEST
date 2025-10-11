using System;
using UnityEngine;

namespace ProductionCells.StaticData
{
	[Serializable]
	public sealed class ProductionCellData : IProductionCellData
	{
		[SerializeField] Color _color;
		[Range(0, 1)]
		[SerializeField] float _productionModifier;
		[SerializeField] float _productionTimer;

		public Color Color => _color;
		public float ProductionModifier => _productionModifier;
		public float ProductionTimer => _productionTimer;
	}
}