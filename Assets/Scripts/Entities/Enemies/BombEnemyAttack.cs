using UnityEngine;
using System.Collections;

public class BombEnemyAttack : EnemyAttack {
	[SerializeField, NotNull] private GameObject blackHolePrefab;

	public override void Attack(Vector3 targetPosition){
		BlackHoleProjectile proj = Instantiate(blackHolePrefab, transform.position, Quaternion.identity).GetComponent<BlackHoleProjectile>();
		proj.expansionTime = 2 / 3f;
		proj.maxScale = 3f;
		proj.StartCoroutine(proj.Explode());
		StartCoroutine(CopyPosition(proj.transform));
	}

    public override float GetCooldown()
    {
		return 1f;
	}

	private IEnumerator CopyPosition(Transform target)
    {
		while (target != null)
        {
			target.position = transform.position;
			yield return null;
        }
    }
}