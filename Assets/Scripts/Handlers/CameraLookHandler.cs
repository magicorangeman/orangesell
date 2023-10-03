using Scripts;
using UnityEngine;

public sealed class CameraLookHandler : MonoBehaviour
{
	[SerializeField] private InputHandler _input;
	[SerializeField] private Transform _myCamera;
	[SerializeField] private float _xMin;
	[SerializeField] private float _xMax;

	private Vector3 _eulerAngles;

	public void MoveCamera()
	{
		_eulerAngles.x -= _input.MouseY;

		_eulerAngles.x = Mathf.Clamp(_eulerAngles.x, _xMin, _xMax);

		_myCamera.localEulerAngles = _eulerAngles;

		transform.Rotate(0.0f, _input.MouseX, 0.0f, Space.World);
	}
}
