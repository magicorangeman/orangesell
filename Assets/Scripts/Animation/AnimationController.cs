using Scripts;
using UnityEngine;

namespace General
{
	public class AnimationController : MonoBehaviour
	{
		[SerializeField] private Animator _animator;
		[SerializeField] private InputHandler _input;

		private string _currentAnimation;
	
		private void Update()
		{
			if (!Mathf.Approximately(_input.Horizontal + _input.Vertical, 0))
			{
				ChangeAnimation("Run");
			}
			else
			{
				ChangeAnimation("Idle");
			}
		}

		private void ChangeAnimation(string animationName)
		{
			if (_currentAnimation == animationName) return;
		
			_animator.Play(animationName);
			_currentAnimation = animationName;
		}
	}
}
