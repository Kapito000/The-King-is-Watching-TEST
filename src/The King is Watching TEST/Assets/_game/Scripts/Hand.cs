using UnityEngine;
using UnityEngine.Assertions;

public sealed class Hand : MonoBehaviour
{
	[SerializeField] bool _isCaptured;
	[SerializeField] GameManager _gameManager;
	[SerializeField] ItemInputController _inputController;

	IItem _captured;

	void Awake()
	{
		Assert.IsNotNull(_gameManager);
		Assert.IsNotNull(_inputController);

		_inputController.Put += OnPut;
		_inputController.Taked += OnTaked;
		_inputController.Rotate += OnRotate;
	}

	void OnDestroy()
	{
		_inputController.Put -= OnPut;
		_inputController.Taked -= OnTaked;
		_inputController.Rotate -= OnRotate;
	}

	void Update()
	{
		if (_isCaptured == false)
			return;

		_captured.MoveTo(_inputController.WorldPos);
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

		_captured.Rotate();
	}

	void OnPut(IFieldCell fieldCell)
	{
		if (_isCaptured == false)
			return;

		if (_gameManager.TryPlace(fieldCell.Pos, _captured) == false)
		{ }
	}

	void CaptureItem(IItem item)
	{
		_captured = item;
		_captured.Capture();
		_isCaptured = true;
	}

	void DropItem()
	{
		_captured.Uncapture();
		_isCaptured = false;
	}
}