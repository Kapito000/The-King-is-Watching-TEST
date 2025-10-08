using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
	public abstract class Grid<T> : IGrid<T>
	{
		readonly T[,] _cells;

		public Vector2Int Size { get; }

		protected Grid(int xSize, int ySize)
		{
			Size = new Vector2Int(xSize, ySize);
			_cells = new T[xSize, ySize];
		}

		public bool Has(int x, int y) =>
			0 <= x && x < Size.x &&
			0 <= y && y < Size.y;

		public bool TrySet(T value, int x, int y)
		{
			if (Has(x, y) == false)
				return false;

			_cells[x, y] = value;
			return true;
		}

		public bool TryGet(int x, int y, out T value)
		{
			if (Has(x, y) == false)
			{
				value = default;
				return false;
			}

			value = _cells[x, y];
			return true;
		}

		public IEnumerable<(Vector2Int cell, T value)> WithValues()
		{
			foreach (var cell in this)
				yield return new(cell, _cells[cell.x, cell.y]);
		}

		public IEnumerable<(Vector2Int cell, T value)> WithValues(
			Func<T, bool> where)
		{
			if (where == null)
			{
				Debug.LogError("The condition is null.");
				yield break;
			}

			foreach (var cell in this)
				if (where.Invoke(_cells[cell.x, cell.y]))
					yield return new(cell, _cells[cell.x, cell.y]);
		}

		public IEnumerable<Vector2Int> AllCoordinates(Func<T, bool> where)
		{
			if (where == null)
			{
				Debug.LogError("The condition is null.");
				yield break;
			}

			foreach (var cell in this)
				if (where.Invoke(_cells[cell.x, cell.y]))
					yield return cell;
		}

		public IEnumerator<Vector2Int> GetEnumerator()
		{
			for (int x = 0; x < Size.x; x++)
			for (int y = 0; y < Size.y; y++)
				yield return new Vector2Int(x, y);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}