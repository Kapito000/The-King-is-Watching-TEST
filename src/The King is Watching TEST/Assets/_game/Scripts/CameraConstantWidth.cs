using UnityEngine;

/// <summary>
/// Keeps constant camera width instead of height, works for both Orthographic & Perspective cameras
/// Made for tutorial https://youtu.be/0cmxFjP375Y
/// </summary>
public class CameraConstantWidth : MonoBehaviour
{
	public Vector2 DefaultResolution = new Vector2(720, 1280);
	[Range(0f, 1f)] public float WidthOrHeight = 0;

	Camera componentCamera;

	float initialSize;
	float targetAspect;

	float initialFov;
	float horizontalFov = 120f;

	void Start()
	{
		componentCamera = GetComponent<Camera>();
		initialSize = componentCamera.orthographicSize;

		targetAspect = DefaultResolution.x / DefaultResolution.y;

		initialFov = componentCamera.fieldOfView;
		horizontalFov = CalcVerticalFov(initialFov, 1 / targetAspect);
	}

	void Update()
	{
		if (componentCamera.orthographic)
		{
			float constantWidthSize =
				initialSize * (targetAspect / componentCamera.aspect);
			componentCamera.orthographicSize =
				Mathf.Lerp(constantWidthSize, initialSize, WidthOrHeight);
		}
		else
		{
			float constantWidthFov =
				CalcVerticalFov(horizontalFov, componentCamera.aspect);
			componentCamera.fieldOfView =
				Mathf.Lerp(constantWidthFov, initialFov, WidthOrHeight);
		}
	}

	float CalcVerticalFov(float hFovInDeg, float aspectRatio)
	{
		float hFovInRads = hFovInDeg * Mathf.Deg2Rad;

		float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

		return vFovInRads * Mathf.Rad2Deg;
	}
}