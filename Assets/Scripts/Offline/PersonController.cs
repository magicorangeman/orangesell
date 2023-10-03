using General.Handlers;
using UnityEngine;

public sealed class PersonController : MonoBehaviour
{
	[SerializeField] private CharacterController _characterController;
	[SerializeField] private MovementHandler _movement;
	[SerializeField] private CameraLookHandler _look;
	[SerializeField] private Transform _myCamera;

	private void Update()
	{
		_characterController.Move(_movement.Velocity * Time.deltaTime);
		_look.MoveCamera();
	}
}
