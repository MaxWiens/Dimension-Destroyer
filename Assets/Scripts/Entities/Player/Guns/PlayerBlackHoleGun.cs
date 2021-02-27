using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackHoleGun : AbstractGun
{
	[SerializeField, NotNull] private GameObject blackHolePrefab;

    public override string Name => "Void Gun";

    private void OnEnable()
	{
		_inputs.Shoot += ShootBlackHoleGun;
	}

	private void OnDisable()
	{
		_inputs.Shoot -= ShootBlackHoleGun;
	}

	private void ShootBlackHoleGun(bool pressed)
    {
		if (pressed)
		{
			Vector3 vel = Camera.main.transform.forward * 3;
			Rigidbody rigidbody = Instantiate(blackHolePrefab, transform.position + vel / 3f, Quaternion.identity).GetComponent<Rigidbody>();
			rigidbody.velocity = vel;
		}
    }
}
