using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlayerGun : MonoBehaviour
{
    public abstract string Name { get; }
    public abstract float Shots { get; }

    public float Cooldown { get; protected set; }

    [SerializeField, NotNull] protected InputManagerSO _inputs;
	[SerializeField, NotNull] protected PlayerStats playerStats;

    protected IEnumerator DoCooldown(float time)
    {
        Cooldown = time;
        while (Cooldown > 0)
        {
            yield return null;
            Cooldown -= Time.deltaTime;
            if (Cooldown < 0)
                Cooldown = 0;
        }
    }
}
