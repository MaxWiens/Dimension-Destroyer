using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackHoleGun : AbstractGun
{
	[SerializeField, NotNull] private GameObject blackHolePrefab;
	private bool onCooldown;

	public override string Name => "Void Gun";

	private void Start()
	{
		onCooldown = false;
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
		if (pressed && !onCooldown)
		{
			StartCoroutine(DoCooldown());
			Vector3 vel = Camera.main.transform.forward * 3;
			Rigidbody rigidbody = Instantiate(blackHolePrefab, transform.position + vel / 3f, Quaternion.identity).GetComponent<Rigidbody>();
			rigidbody.velocity = vel;
		}
	}

	private IEnumerator DoCooldown()
	{
		onCooldown = true;
		yield return new WaitForSeconds(2);
		onCooldown = false;
	}
}
