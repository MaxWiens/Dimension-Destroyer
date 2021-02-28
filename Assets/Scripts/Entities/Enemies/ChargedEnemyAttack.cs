using System;
using System.Collections.Generic;
using UnityEngine;

abstract class ChargedEnemyAttack : EnemyAttack
{
    public abstract float ChargeTime { get; }

    public abstract void Charge(Vector3 target);
    public abstract void EndCharge();
}
