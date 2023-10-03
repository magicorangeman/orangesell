using Scripts;
using UnityEngine;

namespace General.Handlers
{
	public sealed class MovementHandler : MonoBehaviour
	{
		[SerializeField] private CharacterController _characterController;
		[SerializeField] private InputHandler _input;
		[SerializeField] private float _speed;
		[SerializeField] private float _jumpSpeed;
		[SerializeField] private float _gravityScale;

		private bool isGrounded;
		
		public Vector3 Velocity => GetVelocityByInput();

		private Vector3 GetVelocityByInput()
		{
			var verticalVelocity = transform.forward * _input.Vertical * _speed;
			var horizontalVelocity = transform.right * _input.Horizontal * _speed;
		
			Vector3 desiredVelocity = Vector3.ClampMagnitude(verticalVelocity + horizontalVelocity, _speed);
			desiredVelocity.y = _characterController.velocity.y;
		
			if (_characterController.isGrounded)
			{
				desiredVelocity.y = _input.Jump ? _jumpSpeed : 0f;
			}
			else
			{
				desiredVelocity.y += Physics.gravity.y * _gravityScale * Time.deltaTime;
			}

			return desiredVelocity;
		}
	}
}
