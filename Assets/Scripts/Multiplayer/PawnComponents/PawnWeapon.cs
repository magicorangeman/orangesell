using FishNet.Object;
using Scripts;
using UnityEngine;

public sealed class PawnWeapon : NetworkBehaviour
{
	[SerializeField] private InputHandler _input;
	[SerializeField] private Transform _firePoint;
	[SerializeField] private float _damage;
	[SerializeField] private float _shotDelay;
	
	private float _timeUntilNextShot;

	private void Update()
	{
		if (!IsOwner) return;

		if (_timeUntilNextShot <= 0.0f)
		{
			if (_input.Fire)
			{
				ServerFire(_firePoint.position, _firePoint.forward);
				_timeUntilNextShot = _shotDelay;
			}
		}
		else
		{
			_timeUntilNextShot -= Time.deltaTime;
		}
	}

	[ServerRpc]
	private void ServerFire(Vector3 firePointPosition, Vector3 firePointDirection)
	{
		if (Physics.Raycast(firePointPosition, firePointDirection, out RaycastHit hit) 
		    && hit.transform.TryGetComponent(out Pawn pawn))
		{
			pawn.ReceiveDamage(_damage);
		}
	}
}
