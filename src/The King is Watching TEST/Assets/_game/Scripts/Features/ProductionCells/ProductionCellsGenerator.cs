using System.Collections.Generic;
using ProductionCells.StaticData;
using TetrisFields;
using UnityEngine;
using Zenject;

namespace ProductionCells
{
	public sealed class ProductionCellsGenerator : IProductionCellsGenerator
	{
		const int _badCellsDataIndex = 2;
		const int _middleCellsDataIndex = 1;
		const int _bestCellsDataIndex = 0;

		[Inject] IProductionCellDataCollection _dataCollection;

		public void Generate(ITetrisField field)
		{
			GenerateBad(field);
			GenerateBest(field);
			GenerateMiddle();
		}

		void GenerateBad(ITetrisField field)
		{
			if (TryGetData(_badCellsDataIndex, out var data) == false)
				return;

			var size = field.Size;
			var coords = new List<Vector2Int>();

			// Lower side.
			for (int x = 0; x < size.x; x++)
				coords.Add(new Vector2Int(x, 0));

			// Upper side.
			for (int x = 0; x < size.x; x++)
				coords.Add(new Vector2Int(x, size.y - 1));

			// Left side.
			for (int y = 1; y < size.y - 1; y++)
				coords.Add(new Vector2Int(0, y));

			// Right side.
			for (int y = 1; y < size.y - 1; y++)
				coords.Add(new Vector2Int(size.x - 1, y));

			foreach (var coord in coords)
			{
				field.CreateProductionCell(coord, data);
			}
		}

		void GenerateBest(ITetrisField field)
		{ }

		void GenerateMiddle()
		{ }

		bool TryGetData(int index, out ProductionCellData data)
		{
			if (index >= _dataCollection.ProductionData.Length)
			{
				Debug.LogError("No production data found.");
				data = default;
				return false;
			}

			data = _dataCollection.ProductionData[index];
			return true;
		}
	}
}