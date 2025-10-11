using System;
using ProductionCells.StaticData;
using UnityEngine;
using UnityEngine.Assertions;

namespace ProductionCells
{
	public sealed class ProductionCell : MonoBehaviour, IProductionCell
	{
		[SerializeField] SpriteRenderer _spriteRenderer;
		
		IProductionCellData _data;

		void Awake()
		{
			Assert.IsNotNull(_spriteRenderer);
		}

		public void Init(IProductionCellData data)
		{
			_data	= data;
			_spriteRenderer.color = _data.Color;
		}
	}
}