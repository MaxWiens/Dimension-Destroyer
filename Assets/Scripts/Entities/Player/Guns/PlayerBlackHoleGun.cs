using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackHoleGun : AbstractPlayerGun
{
	[SerializeField, NotNull] private GameObject blackHolePrefab;

	public override string Name => "Void Gun";

    public override float Shots => 99;

    private void Start()
	{
		Cooldown = 0;
	}

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
		if (pressed && Cooldown <= 0 && Shots >= 1)
		{
			StartCoroutine(DoCooldown(2));
			Vector3 vel = Camera.main.transform.forward * 5;
			Rigidbody rigidbody = Instantiate(blackHolePrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
			rigidbody.velocity = vel;
		}
	}
}
