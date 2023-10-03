using FishNet.Object;
using General;
using General.Handlers;
using UnityEngine;

public sealed class PawnController : NetworkBehaviour
{
	[SerializeField] private CharacterController _characterController;
	[SerializeField] private MovementHandler _movement;
	[SerializeField] private CameraLookHandler _look;
	[SerializeField] private Transform _myCamera;
	[SerializeField] private AnimationControllerOnline _animation;
	
	public override void OnStartClient()
	{
		base.OnStartClient();
		_myCamera.GetComponent<Camera>().enabled = IsOwner;
		_myCamera.GetComponent<AudioListener>().enabled = IsOwner;
	}
	private void Update()
	{
		if (!IsOwner) return;
		
		_characterController.Move(_movement.Velocity * Time.deltaTime);
		_look.MoveCamera();
		
		if (!Mathf.Approximately(_movement.Velocity.x + _movement.Velocity.z, 0))
		{
			_animation.ChangeAnimation("Run");
		}
		else
		{
			_animation.ChangeAnimation("Idle");
		}
	}
}
