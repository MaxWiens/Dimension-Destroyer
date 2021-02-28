using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTheGunThatCanKillReality : AbstractPlayerGun
{
    public override string Name => "The Eraser";

    public override float Shots => playerStats.energyCells / 5f;

	private void Start()
	{
		Cooldown = 0;
		if (GameObject.FindGameObjectWithTag("Destructible Holder") == null)
        {
			Debug.LogError("The Eraser will not work, Destructible Holder was not found.");
        }
	}

	private void OnEnable()
	{
		_inputs.Shoot += ShootTheGunThatCanKillReality;
	}

	private void OnDisable()
	{
		_inputs.Shoot -= ShootTheGunThatCanKillReality;
	}

	private void ShootTheGunThatCanKillReality(bool pressed)
	{
		if (pressed && Cooldown <= 0 && Shots >= 1)
		{
			GameObject destructibleHolder = GameObject.FindGameObjectWithTag("Destructible Holder");
			int childCount = destructibleHolder.transform.childCount;
			for (int i = 0; i < childCount; i++)
            {
				Destroy(destructibleHolder.transform.GetChild(i).gameObject);
            }

			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach (GameObject enemy in enemies)
            {
				Destroy(enemy);
            }
		}
	}
}
