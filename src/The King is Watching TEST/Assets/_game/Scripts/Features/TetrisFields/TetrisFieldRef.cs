using UnityEngine;

namespace TetrisFields
{
	public sealed class TetrisFieldRef : MonoBehaviour, ITetrisFieldRef
	{
		public ITetrisField Field { get; set; }
	}
}