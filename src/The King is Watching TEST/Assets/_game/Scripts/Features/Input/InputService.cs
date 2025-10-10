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

		readonly Subject<Unit> _rotate = new();
		readonly Subject<Vector2> _clickedSubject = new();

		public IObservable<Unit> Rotate => _rotate;
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

			Observable
				.FromEvent<InputAction.CallbackContext>(
					h => _inputActions.Base.Rotate.performed += h,
					h => _inputActions.Base.Rotate.performed -= h)
				.Subscribe(_ => _rotate.OnNext(Unit.Default))
				.AddTo(_disposables);
		}

		void IDisposable.Dispose()
		{
			_rotate.OnCompleted();
			_clickedSubject.OnCompleted();
			
			_disposables.Dispose();
		}
	}
}