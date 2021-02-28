using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeamGun : AbstractGun
{
	[SerializeField, NotNull] private GameObject beamPrefab;

	private GameObject beamInstance;
	private bool onCooldown;

    public override string Name => "Beam Gun";

    private void Start()
    {
		onCooldown = false;
	}

    private void OnEnable()
	{
		_inputs.Shoot += ShootBeamGun;
		beamInstance = null;
	}

	private void OnDisable()
	{
		_inputs.Shoot -= ShootBeamGun;
	}

	private void ShootBeamGun(bool pressed)
	{
		if (pressed && !onCooldown && beamInstance == null && (playerStats.lenses > 0 || true))
		{
			playerStats.lenses--;
			StartCoroutine(DoCooldown());
			beamInstance = Instantiate(beamPrefab, transform.position, Quaternion.identity);
			BeamProjectile script = beamInstance.GetComponent<BeamProjectile>();
			script.SetPosition(gameObject, Camera.main.transform);
		}
	}

	private IEnumerator DoCooldown()
    {
		onCooldown = true;
		yield return new WaitForSeconds(2);
		onCooldown = false;
    }
}
