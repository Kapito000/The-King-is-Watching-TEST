using Infrastructure;
using Input;
using TetrisFields;
using TetrisFields.Items;
using UniRx;
using UnityEngine;
using Zenject;

public sealed class Hand : MonoBehaviour
{
	[SerializeField] bool _isCaptured;

	[Inject] IInputService _inputService;

	[Inject(Id = InjectId.MainCamera)]
	Camera _camera;

	IItem _capturedItem;

	void Update()
	{
		TryReplaceCapturedItem();
	}

	public void Init()
	{
		_inputService.Clicked
			.Subscribe(OnClicked)
			.AddTo(this);
	}

	void TryReplaceCapturedItem()
	{
		if (_isCaptured == false)
			return;

		var worldPos = ScreenToWorldPoint(_inputService.Pos);
		_capturedItem.ReplaceTo(worldPos);
	}

	void OnClicked(Vector2 mousePos)
	{
		if (TryCaptureItem(mousePos))
			return;

		if (TryPutItem(mousePos))
			return;
	}

	bool TryCaptureItem(Vector2 pos)
	{
		if (_isCaptured)
			return false;

		Vector2 worldPos = ScreenToWorldPoint(pos);
		RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

		if (hit.collider == null ||
		    !hit.collider.TryGetComponent<IFieldCell>(out var fieldCell) ||
		    !hit.collider.TryGetComponent<ITetrisFieldRef>(out var fieldRef) ||
		    fieldRef.Field == null)
			return false;

		var field = fieldRef.Field;
		
		if (field.HasItemAt(fieldCell.FieldPos) == false)
			return false;

		var item = field.GetItemAt(fieldCell.FieldPos);
		field.ExtractItem(item);

		CaptureItem(item);
		return true;
	}

	bool TryPutItem(Vector2 pos)
	{
		if (_isCaptured == false)
			return false;

		Vector2 worldPos = ScreenToWorldPoint(pos);
		RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

		if (hit.collider == null ||
		    !hit.collider.TryGetComponent<IFieldCell>(out var fieldCell) ||
		    !hit.collider.TryGetComponent<ITetrisFieldRef>(out var fieldRef) ||
		    fieldRef.Field == null)
			return false;

		var field = fieldRef.Field;

		if (field.CanPutItem(_capturedItem, fieldCell.FieldPos) == false)
			return false;

		field.PutItem(_capturedItem, fieldCell.FieldPos);

		DropItem();
		return true;
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
		_capturedItem = item;
		_isCaptured = true;
	}

	void DropItem()
	{
		_isCaptured = false;
	}

	Vector3 ScreenToWorldPoint(Vector2 pos) =>
		_camera.ScreenToWorldPoint(pos);
}