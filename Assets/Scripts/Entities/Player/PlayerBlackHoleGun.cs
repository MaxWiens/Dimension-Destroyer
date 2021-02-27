using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackHoleGun : MonoBehaviour
{
	[SerializeField, NotNull] private GameObject blackHolePrefab;
	[SerializeField, NotNull] private InputManagerSO _inputs;

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
			Vector3 vel = Camera.main.transform.forward;
			Rigidbody rigidbody = Instantiate(blackHolePrefab, transform.position + vel, Quaternion.identity).GetComponent<Rigidbody>();
			rigidbody.velocity = vel;
		}
    }
}
