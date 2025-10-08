using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public sealed class ItemInputController : MonoBehaviour
{
	[SerializeField] Vector2 _pos;

	void OnPos(InputValue value)
	{
		_pos = value.Get<Vector2>();
	}

	void OnTake()
	{
		Vector2 worldPos = Camera.main.ScreenToWorldPoint(_pos);
		RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

		if (hit.collider != null)
		{
			Debug.Log("Клик по: " + hit.collider.gameObject.name);
		}
	}


	void OnRotate()
	{ }
}