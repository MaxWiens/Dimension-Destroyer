using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities.Enemies
{
    class BeamGunEnemy : ChargedEnemyAttack
    {
        [SerializeField, NotNull] private GameObject beamPrefab;
        [SerializeField, NotNull] private GameObject chargeEffectPrefab;
        [SerializeField] private AudioSource chargeSound;

        public override float ChargeTime => 1.85f;

        private GameObject chargeEffectInstance;

        public override void Attack(Vector3 targetPosition)
        {
            transform.LookAt(targetPosition);
            GameObject beamInstance = Instantiate(beamPrefab, transform.position, Quaternion.identity);
            BeamProjectile script = beamInstance.GetComponent<BeamProjectile>();
            script.SetPosition(gameObject, transform);
        }

        public override void Charge(Vector3 target)
        {
            transform.LookAt(target);
            chargeEffectInstance = Instantiate(chargeEffectPrefab, transform);
            chargeSound.Play();
        }

        public override void EndCharge()
        {
            Destroy(chargeEffectInstance);
        }

        public override float GetCooldown()
        {
            return Random.Range(30f, 45f);
        }
    }
}
