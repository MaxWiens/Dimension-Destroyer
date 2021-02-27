using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGun : MonoBehaviour
{
    public abstract string Name { get; }

	[SerializeField, NotNull] protected InputManagerSO _inputs;
	[SerializeField, NotNull] protected PlayerStats playerStats;
}
