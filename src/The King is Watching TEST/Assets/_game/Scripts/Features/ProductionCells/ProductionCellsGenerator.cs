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
			GenerateMiddle(field);
			GenerateBest(field);
		}

		void GenerateBad(ITetrisField field)
		{
			var dataIndex = _badCellsDataIndex;
			if (TryGetData(dataIndex, out var data) == false)
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
				field.CreateProductionCell(coord, data, dataIndex);
			}
		}

		void GenerateMiddle(ITetrisField field)
		{
			var dataIndex = _middleCellsDataIndex;
			if (TryGetData(dataIndex, out var data) == false)
				return;

			var size = field.Size;
			var coords = new List<Vector2Int>();

			int lines = (int)Mathf.Floor((float)size.x / 3);
			if (lines == 0)
				lines = 1;

			for (int l = 0; l < lines; l++)
			{
				// Lower side.
				for (int x = 1; x < size.x - 1; x++)
					coords.Add(new Vector2Int(x, 1 + l));

				// Upper side.
				for (int x = 1; x < size.x - 1; x++)
					coords.Add(new Vector2Int(x, size.y - 2 - l));

				// Left side.
				for (int y = 2 + l; y < size.y - 2; y++)
					coords.Add(new Vector2Int(1 + l, y));

				// Right side.
				for (int y = 2 + l; y < size.y - 2; y++)
					coords.Add(new Vector2Int(size.x - 2 - l, y));
			}

			foreach (var coord in coords)
			{
				field.CreateProductionCell(coord, data, dataIndex);
			}
		}

		void GenerateBest(ITetrisField field)
		{
			var dataIndex = _bestCellsDataIndex;
			if (TryGetData(dataIndex, out var data) == false)
				return;

			var coords = field.AllProductionCellsCoordinates(pc => pc == null);

			foreach (var coord in coords)
			{
				field.CreateProductionCell(coord, data, dataIndex);
			}
		}

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