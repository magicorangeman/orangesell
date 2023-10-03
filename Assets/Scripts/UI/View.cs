using UnityEngine;

public abstract class View : MonoBehaviour
{
	public bool Initialized { get; private set; }

	public virtual void Initialize()
	{
		Initialized = true;
	}
}
