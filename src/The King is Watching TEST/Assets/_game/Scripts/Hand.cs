using Input;
using TetrisFields;
using TetrisFields.Items;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using Zenject;

public sealed class Hand : MonoBehaviour
{
	[SerializeField] bool _isCaptured;
	[SerializeField] GameBootstrapper gameBootstrapper;
	[Inject] IInputService _inputService;

	IItem _captured;

	void OnDestroy()
	{ }

	void Update()
	{
		if (_isCaptured == false)
			return;

		// _captured.MoveTo(_inputController.WorldPos);
	}
	
	

	void OnTaked(IItem item)
	{
		if (_isCaptured)
			return;

		CaptureItem(item);
	}

	void OnRotate()
	{
		if (_isCaptured)
			return;

		// _captured.Rotate();
	}

	void OnPut(IFieldCell fieldCell)
	{
		if (_isCaptured == false)
			return;

		// if (_gameManager.TryPlace(fieldCell, _captured) == false)
		// return;

		DropItem();
	}

	void CaptureItem(IItem item)
	{
		_captured = item;
		// _captured.Capture();
		_isCaptured = true;
	}

	void DropItem()
	{
		// _captured.Uncapture();
		_isCaptured = false;
	}
}