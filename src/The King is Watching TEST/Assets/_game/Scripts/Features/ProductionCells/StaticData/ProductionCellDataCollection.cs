using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menu = Constant.CreateAssetMenu.MenuName;

namespace ProductionCells.StaticData
{
	[CreateAssetMenu(menuName =
		Menu.StaticData + nameof(ProductionCellDataCollection))]
	public sealed class ProductionCellDataCollection : ScriptableObject,
		IProductionCellDataCollection
	{
		[SerializeField] ProductionCellData[] _productionData;

		public ProductionCellData[] ProductionData => _productionData;

		public IEnumerator<ProductionCellData> GetEnumerator()
		{
			foreach (var data in _productionData)
				yield return data;
		}

		IEnumerator IEnumerable.GetEnumerator() =>
			GetEnumerator();
	}
}