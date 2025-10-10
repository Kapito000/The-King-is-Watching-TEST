using Infrastructure;
using Input;
using ItemDestruction;
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

		_inputService.Rotate
			.Subscribe(_ => RotateItem())
			.AddTo(this);
	}

	void RotateItem()
	{
		if (_isCaptured == false)
			return;

		_capturedItem.Rotate();
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

		if (TryDestructItem(mousePos))
			return;
	}

	bool TryDestructItem(Vector2 pos)
	{
		if (_isCaptured == false)
			return false;

		var hit = RayCastHit(pos);

		if (hit.collider == null ||
		    !hit.collider.TryGetComponent<IDestroyArea>(out var destroyArea))
			return false;

		_capturedItem.Destroy();
		DropItem();
		
		return true;
	}

	bool TryCaptureItem(Vector2 pos)
	{
		if (_isCaptured)
			return false;

		var hit = RayCastHit(pos);

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

		var hit = RayCastHit(pos);

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

	void CaptureItem(IItem item)
	{
		_isCaptured = true;
		_capturedItem = item;
		_capturedItem.Capture();
	}

	void DropItem()
	{
		_isCaptured = false;
		_capturedItem.Uncapture();
	}

	RaycastHit2D RayCastHit(Vector2 pos)
	{
		Vector2 worldPos = ScreenToWorldPoint(pos);
		return Physics2D.Raycast(worldPos, Vector2.zero);
	}

	Vector3 ScreenToWorldPoint(Vector2 pos) =>
		_camera.ScreenToWorldPoint(pos);
}