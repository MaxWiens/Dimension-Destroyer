using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherGun : AbstractPlayerGun
{
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip launchSound;

	public override string Name => "Launcher";
	public override float Shots => 99;

	private Rigidbody rb = default;

	private void Start(){
		Cooldown = 0;
		rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
	}

	private void OnEnable() {
		_inputs.Shoot += ShootGun;
	}

	private void OnDisable()
	{
		_inputs.Shoot -= ShootGun;
	}

	private void ShootGun(bool pressed)
	{
		if (pressed && Cooldown <= 0 && (Shots >= 1))
		{
			StartCoroutine(DoCooldown(1.2f));
			if(launchSound != null) audioSource?.PlayOneShot(launchSound);
			rb.AddForce(-30f*Camera.main.transform.forward, ForceMode.Impulse);
		}
	}
}
