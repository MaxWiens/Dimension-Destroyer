using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
	[SerializeField, NotNull] private NavMeshAgent _agent = default;

	private Transform _playerTransform = default;

	private void OnEnable() {
		if(_playerTransform == null)
			_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

	}

	private void OnDisable() {

	}

	private void FixedUpdate() {
		FollowPlayer();
	}

	public void FollowPlayer(){
		if(_agent.SetDestination(_playerTransform.position)){

		}else{

		}
	}
}