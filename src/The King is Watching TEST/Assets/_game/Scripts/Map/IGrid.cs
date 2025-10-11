using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
	public interface IGrid<T> : IEnumerable<T>
	{
		public T this[int x, int y] { get; set; }
		Vector2Int Size { get; }
		bool HasCell(int x, int y);
		bool TrySet(T value, int x, int y);
		bool TryGet(int x, int y, out T value);
		IEnumerable<(Vector2Int cell, T value)> WithValues();

		public IEnumerable<(Vector2Int cell, T value)> WithValues(
			Func<T, bool> where);

		IEnumerable<Vector2Int> AllCoordinates(Func<T, bool> where);
		IEnumerable<Vector2Int> AllCoordinates();
	}
}