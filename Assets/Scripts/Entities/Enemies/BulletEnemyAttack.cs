using UnityEngine;

public class BulletEnemyAttack : EnemyAttack {
	[SerializeField, NotNull] private GameObject _bulletPrefab = default;
	[SerializeField] private float _bulletVelocity = 10f;
	public override void Attack(Vector3 targetPosition){
		Vector3 vel = (targetPosition-transform.position).normalized * _bulletVelocity;
		Instantiate(_bulletPrefab, transform.position+vel, Quaternion.identity).GetComponent<Rigidbody>().velocity = vel;
	}
}