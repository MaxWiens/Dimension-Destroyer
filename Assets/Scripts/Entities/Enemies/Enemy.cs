using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public enum EnemyState {
	Wandering,
	Seeking,
	Chasing,
	WithinRange,
}

public class Enemy : MonoBehaviour {
	public EnemyState State {get; private set;} = EnemyState.Wandering;
	public Transform PlayerTransform { set => _playerTransform = value; }

	[SerializeField, NotNull] private NavMeshAgent _agent = default;
	[SerializeField] private float _speed = 5f;

	[Header("Wandering")]
	[SerializeField] private float _wanderSpeed = 2.5f;
	[SerializeField] private float _minWanderTime = 0.5f;
	[SerializeField] private float _maxWanderTime = 4f;

	[Header("Seeking")]
	[SerializeField] private float _detectionRange = 30f;
	[SerializeField] private float _agroTime = 5f;

	[Header("WithinRange")]
	[SerializeField] private float _minAttackRange = 10f;
	[SerializeField] private float _maxAttackRange = 15f;

	private float _agroTimer = 0f;
	private Transform _playerTransform = default;
	private float _wanderTime = 0f;
	private float _wanderTimer = 0f;
	private Vector3 _wanderMoveVec = Vector3.zero;

	private void OnEnable() {
		if(_playerTransform == null) _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void FixedUpdate() {
		Vector3 dif = _playerTransform.position - transform.position;
		switch(State){
			case EnemyState.Wandering:
				// check if enemy detects player
				if(dif.sqrMagnitude <= _detectionRange*_detectionRange){
					State = EnemyState.Seeking;
					_wanderTime = 0f;
					_wanderTimer = 0f;
				}else
					Wander(Time.fixedDeltaTime);
				break;
			case EnemyState.Seeking:
				// check distance from player
				if(dif.sqrMagnitude > _detectionRange*_detectionRange)
					UpdateAgro(Time.fixedDeltaTime);
				else
					FollowPlayer();
				break;
			case EnemyState.WithinRange:
				if(dif.sqrMagnitude > _maxAttackRange*_maxAttackRange){
					State = EnemyState.Seeking;
				}
				break;
		}
		Debug.Log(State);
	}

	private void UpdateAgro(float deltaTime){
		_agroTimer += deltaTime;
		if(_agroTimer >= _agroTime){
			_agroTimer = 0f;
			State = EnemyState.Wandering;
			_agent.ResetPath();
		}
	}

	private void FollowPlayer(){
		if(!_agent.SetDestination(_playerTransform.position)){
			Debug.LogWarning("Enemy failed to find path to player :(");
		}
	}

	private void Wander(float deltaTime){
		_wanderTimer += deltaTime;
		if(_wanderTimer >= _wanderTime){
			_wanderTimer -= _wanderTime;
			_wanderTime = UnityEngine.Random.Range(_minWanderTime, _maxWanderTime);
			Vector2 v2 = UnityEngine.Random.insideUnitCircle*_wanderSpeed;
			_wanderMoveVec = new Vector3(v2.x, 0f, v2.y);
		}
		_agent.Move(_wanderMoveVec*deltaTime);
	}
}