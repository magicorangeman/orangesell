using FishNet.Component.Animating;
using FishNet.Object;
using UnityEngine;

namespace General
{
	public class AnimationControllerOnline : NetworkBehaviour
	{
		[SerializeField] private NetworkAnimator _networkAnimator;

		private string _currentAnimation;

		public void ChangeAnimation(string animationName)
		{
			if (_currentAnimation == animationName) return;
		
			_networkAnimator.Play(animationName);
			_currentAnimation = animationName;
		}
	}
}
