using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menu = Constant.CreateAssetMenu.MenuName;

namespace ProductionCells.StaticData
{
	[CreateAssetMenu(menuName =
		Menu.StaticData + nameof(ProductionCellDataCollection))]
	public sealed class ProductionCellDataCollection : ScriptableObject,
		IProductionCellDataCollection, IEnumerable<ProductionCellData>
	{
		[SerializeField] ProductionCellData[] _productionCells;

		public IEnumerator<ProductionCellData> GetEnumerator()
		{
			foreach (var data in _productionCells)
				yield return data;
		}

		IEnumerator IEnumerable.GetEnumerator() => 
			GetEnumerator();
	}
}