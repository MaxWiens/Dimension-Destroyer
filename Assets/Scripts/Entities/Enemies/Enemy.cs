using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
	[SerializeField, NotNull] private NavMeshAgent _agent = default;

	private Transform _playerTransform = default;
	public Transform PlayerTransform { set => _playerTransform = value; }

	private void OnEnable() {
		if(_playerTransform == null) _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void FixedUpdate() {
		FollowPlayer();
	}

	public void FollowPlayer(){
		if(!_agent.SetDestination(_playerTransform.position)){
			Debug.LogWarning("Enemy failed to find path to player :(");
		}
	}
}