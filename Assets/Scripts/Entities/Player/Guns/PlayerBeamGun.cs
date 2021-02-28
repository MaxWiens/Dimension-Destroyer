using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeamGun : AbstractPlayerGun
{
	[SerializeField, NotNull] private GameObject beamPrefab;

    public override string Name => "Beam Gun";
	public override float Shots => playerStats.lenses;

	private void Start()
    {
		Cooldown = 0;
	}

    private void OnEnable()
	{
		_inputs.Shoot += ShootBeamGun;
	}

	private void OnDisable()
	{
		_inputs.Shoot -= ShootBeamGun;
	}

	private void ShootBeamGun(bool pressed)
	{
		if (pressed && Cooldown <= 0 && (Shots >= 1))
		{
			playerStats.lenses--;
			StartCoroutine(DoCooldown(1.2f));
			GameObject beamInstance = Instantiate(beamPrefab, transform.position, Quaternion.identity);
			BeamProjectile script = beamInstance.GetComponent<BeamProjectile>();
			script.SetPosition(gameObject, Camera.main.transform);
		}
	}
}
