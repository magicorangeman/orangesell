using UnityEngine;

namespace Scripts
{
	public sealed class InputHandler : MonoBehaviour
	{
		[field: SerializeField] public float Sensitivity { get; private set; }
		public float Horizontal { get; private set; }
		public float Vertical { get; private set; }
		public float MouseX { get; private set; }
		public float MouseY { get; private set; }
		public bool Jump { get; private set; }
		public bool Fire { get; private set; }
	
		private void Update()
		{
			Horizontal = Input.GetAxis("Horizontal");
			Vertical = Input.GetAxis("Vertical");
			MouseX = Input.GetAxis("Mouse X") * Sensitivity;
			MouseY = Input.GetAxis("Mouse Y") * Sensitivity;
			Jump = Input.GetButton("Jump");
			Fire = Input.GetButton("Fire1");
		}
	}
}
