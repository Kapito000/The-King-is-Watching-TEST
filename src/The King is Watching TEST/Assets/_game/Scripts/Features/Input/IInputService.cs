using System;
using UniRx;
using UnityEngine;

namespace Input
{
	public interface IInputService
	{
		Vector2 Pos { get; }
		IObservable<Unit> Rotate { get; }
		IObservable<Vector2> Clicked { get; }
		void Enable();
		void Disable();
	}
}