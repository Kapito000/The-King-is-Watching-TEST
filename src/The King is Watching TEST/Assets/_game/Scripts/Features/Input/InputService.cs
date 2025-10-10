using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Input
{
	public sealed class InputService : IInputService, IDisposable
	{
		[Inject] InputActions _inputActions;

		CompositeDisposable _disposables = new();

		public Vector2 Pos { get; private set; }

		ISubject<Vector2> _clickedSubject = new Subject<Vector2>();

		public IObservable<Vector2> Clicked => _clickedSubject;

		public void Enable()
		{
			_inputActions.Enable();
		}

		public void Disable()
		{
			_inputActions.Disable();
		}

		public void Init()
		{
			Observable
				.FromEvent<InputAction.CallbackContext>(
					h => _inputActions.Base.Pos.performed += h,
					h => _inputActions.Base.Pos.performed -= h)
				.Subscribe(context => Pos = context.ReadValue<Vector2>())
				.AddTo(_disposables);

			Observable
				.FromEvent<InputAction.CallbackContext>(
					h => _inputActions.Base.Click.performed += h,
					h => _inputActions.Base.Click.performed -= h)
				.Subscribe(_ => _clickedSubject.OnNext(Pos))
				.AddTo(_disposables);
		}

		void IDisposable.Dispose()
		{
			_clickedSubject.OnCompleted();
			_disposables.Dispose();
		}
	}
}