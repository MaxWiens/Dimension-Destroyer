using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour {
	public abstract void Attack(Vector3 targetPosition);

	public abstract float GetCooldown();
}