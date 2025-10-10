using System;
using UniRx;
using UnityEngine;

namespace Input
{
	public interface IInputService
	{
		Vector2 Pos { get; }
		IObservable<Vector2> Clicked { get; }
		IObservable<Unit> Rotate { get; }
		void Enable();
		void Disable();
	}
}