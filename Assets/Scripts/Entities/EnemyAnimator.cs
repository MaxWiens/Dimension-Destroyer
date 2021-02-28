using UnityEngine;
using UnityEngine.AI;
public class EnemyAnimator : MonoBehaviour {
	[SerializeField, NotNull] private Animator _animator = default;
	[SerializeField, NotNull] private SpriteRenderer _spriteRender = default;
	[SerializeField, NotNull] private NavMeshAgent _agent = default;

	public enum EnemyAnimationState {
		moveForward = 1,
		moveBackward = 2,
		idleForward = 3,
		idleBackward = 4
	}

	private Vector3 _prevPos = default;
	private bool isBack = false;

	private void Update() {
		Vector3 v = transform.position - _prevPos;
		if(v != Vector3.zero){
			Vector2 camForward = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
			float angle = Vector2.SignedAngle(camForward, new Vector2(v.x, v.z));
			if(angle < 90 && angle > -90){
				// is forward
				_animator.SetInteger("CurID", (int)EnemyAnimationState.moveForward);
				isBack = false;
			}else{
				//is backward
				_animator.SetInteger("CurID", (int)EnemyAnimationState.moveBackward);
				isBack = true;
			}

			if(angle < 0){
				_spriteRender.flipX = true;
			}else{
				_spriteRender.flipX = false;
			}
		}else{
			if(isBack){
				_animator.SetInteger("CurID", (int)EnemyAnimationState.idleBackward);
			}else{
				_animator.SetInteger("CurID", (int)EnemyAnimationState.idleForward);
			}

		 }
		_prevPos = transform.position;
	}

}