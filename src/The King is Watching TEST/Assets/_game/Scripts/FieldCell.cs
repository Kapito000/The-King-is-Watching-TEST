using UnityEngine;
using UnityEngine.Assertions;

public sealed class FieldCell : MonoBehaviour, IFieldCell
{
	IItem _item;

	[SerializeField] Collider2D _collider;

	[field: SerializeField] public bool HasItem { get; private set; }
	[field: SerializeField] public Vector2Int FieldPos { get; set; }

	public Vector2 Pos
	{
		get => transform.position;
		set => transform.position = new Vector3(value.x, value.y, 0);
	}

	void Awake()
	{
		Assert.IsNotNull(_collider);
	}

	public void Place(IItem item)
	{
		HasItem = true;
		_item = item;
		_collider.enabled = false;
	}

	public IItem ExtractItem()
	{
		HasItem = false;
		_collider.enabled = true;
		return _item;
	}
}