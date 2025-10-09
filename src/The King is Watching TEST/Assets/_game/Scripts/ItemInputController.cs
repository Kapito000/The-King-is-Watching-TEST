using System;
using TetrisFields;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public sealed class ItemInputController : MonoBehaviour
{
	[field: SerializeField] public Vector2 WorldPos { get; private set; }
	[field: SerializeField] public Vector2 ScreenPos { get; private set; }

	public event Action Rotate;
	public event Action<IItem> Taked;
	public event Action<IFieldCell> Put;
	public event Action<IItem, IFieldCell> GetedItemFromField;

	void OnPos(InputValue value)
	{
		WorldPos = Camera().ScreenToWorldPoint(ScreenPos);
		ScreenPos = value.Get<Vector2>();
	}

	void OnTake()
	{
		Vector2 worldPos = Camera().ScreenToWorldPoint(ScreenPos);
		RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

		if (hit.collider == null ||
		    hit.collider.TryGetComponent<IItemCell>(out var itemCell) == false ||
		    itemCell.Item == null)
			return;

		Taked?.Invoke(itemCell.Item);
	}

	void OnPut()
	{
		Vector2 worldPos = Camera().ScreenToWorldPoint(ScreenPos);
		RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

		if (hit.collider == null ||
		    hit.collider.TryGetComponent<IFieldCell>(out var fieldCell) == false)
			return;

		Put?.Invoke(fieldCell);
	}

	void OnGetItemFromField()
	{
		Vector2 worldPos = Camera().ScreenToWorldPoint(ScreenPos);
		RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

		if (hit.collider == null ||
		    hit.collider.TryGetComponent<IFieldCell>(out var fieldCell) == false ||
		    fieldCell.HasItem == false)
			return;

		GetedItemFromField?.Invoke(fieldCell.Item, fieldCell);
	}

	void OnRotate()
	{
		Rotate?.Invoke();
	}

	Camera Camera() =>
		UnityEngine.Camera.main;
}