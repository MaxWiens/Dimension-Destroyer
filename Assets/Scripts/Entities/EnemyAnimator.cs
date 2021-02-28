using UnityEngine;
using UnityEngine.AI;
public class EnemyAnimator : MonoBehaviour {
	[SerializeField, NotNull] private Animator _animator = default;
	[SerializeField, NotNull] private NavMeshAgent _agent = default;

	public enum EnemyAnimationState {
		moveForward = 1,
		moveBackward = 2,
		idleForward = 3,
		idleBackward = 4
	}

	private Vector3 _prevPos = default;

	private void Update() {
		if(transform.position != _prevPos){
			_animator.SetInteger("CurID", (int)EnemyAnimationState.moveForward);
		}else{
		 	_animator.SetInteger("CurID", (int)EnemyAnimationState.idleForward);
		 }
		_prevPos = transform.position;
	}

}