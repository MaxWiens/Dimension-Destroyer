using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeamGun : MonoBehaviour
{
	[SerializeField, NotNull] private GameObject beamPrefab;
	[SerializeField, NotNull] private InputManagerSO _inputs;

	private GameObject beamInstance;

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
		if (pressed && beamInstance == null)
		{
			beamInstance = Instantiate(beamPrefab, transform.position, Quaternion.identity);
			BeamProjectile script = beamInstance.GetComponent<BeamProjectile>();
			script.SetPosition(gameObject, Camera.main.transform);
		}
	}
}
