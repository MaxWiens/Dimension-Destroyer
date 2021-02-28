using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public enum EnemyState {
	Wandering,
	Seeking,
	Chasing,
	WithinRange,
	WeaponCharging,
}

public class Enemy : MonoBehaviour {
	public EnemyState State {get; private set;} = EnemyState.Wandering;
	public Transform PlayerTransform { set => _playerTransform = value; }

	[SerializeField, NotNull] private NavMeshAgent _agent = default;
	[SerializeField, NotNull] private EnemyAttack _attack = default;
	[NotNull] public GameObject _navMeshLinkPrefab = default;
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
	[SerializeField] private float _recalculateCooldown = 0f;

	[Header("WeaponCharging")]
	[SerializeField] private float _chargeTimeRemaining = 0f;
	[SerializeField] private Vector3 _targettedPosition = Vector3.zero;

	public const float MAX_DROP_HEIGHT = float.PositiveInfinity;

	private float _agroTimer = 0f;
	private Transform _playerTransform = default;
	private float _wanderTime = 0f;
	private float _wanderTimer = 0f;
	private Vector3 _wanderMoveVec = Vector3.zero;

	private float _attackTimer = 1f;

	private NavMeshLink _link;
	private float _linkLifeTimer = 0f;

	private void OnEnable() {
		if(_playerTransform == null) _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void FixedUpdate() {
		if (_playerTransform == null)
			return;

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
				else if(dif.sqrMagnitude <= _maxAttackRange*_maxAttackRange){
					State = EnemyState.WithinRange;
					_agent.ResetPath();
				}else
					FollowPlayer();
				break;
			case EnemyState.WithinRange:
				if(dif.sqrMagnitude > _maxAttackRange*_maxAttackRange){
					State = EnemyState.Seeking;
				}else{
					if (_recalculateCooldown <= 0)
					{
						// If in the min range
						if (dif.sqrMagnitude < _minAttackRange * _minAttackRange)
						{
							Vector3 goTo = -dif.normalized * ((_minAttackRange + _maxAttackRange) / 2f);
							_agent.SetDestination(transform.position + goTo);
							_recalculateCooldown = .2f;
						}
						// Strafe
						else
                        {
							Quaternion rotation = Quaternion.identity;
							rotation.eulerAngles = new Vector3(0, (Random.Range(0, 2) == 0 ? 1 : -1) * 30, 0);
							Vector3 targetPos = _playerTransform.position - (rotation * dif);
							_agent.SetDestination(targetPos);
							_recalculateCooldown = .5f;
						}
                    }
					else
                    {
						_recalculateCooldown -= Time.deltaTime;
					}

					if(_attackTimer <= 0f){
						if (_attack is ChargedEnemyAttack ca)
                        {
							State = EnemyState.WeaponCharging;
							_chargeTimeRemaining = ca.ChargeTime;
							_targettedPosition = _playerTransform.position;
							ca.Charge(_playerTransform.position);
							_agent.enabled = false;
						}
						else
						{
							_targettedPosition = _playerTransform.position;
							Attack();
                        }
					}
					_attackTimer -= Time.fixedDeltaTime;
				}
				break;
			case EnemyState.WeaponCharging:
				_chargeTimeRemaining -= Time.deltaTime;
				if (_chargeTimeRemaining <= 0)
                {
					if (_attack is ChargedEnemyAttack ca)
					{
						ca.EndCharge();
					}
					else
                    {
						Debug.LogError($"{gameObject} had WeaponCharging state, but attack is not a ChargedEnemyAttack");
                    }

					_agent.enabled = true;
					Attack();
					State = EnemyState.WithinRange;
				}
				break;
		}
		if(_link != null && _link.enabled){
			_linkLifeTimer -= Time.fixedDeltaTime;
			if(_linkLifeTimer <= 0f){
				_link.enabled = false;
			}
		}
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
		if(_agent.remainingDistance <= 0.001f && _playerTransform.position.y < _agent.pathEndPosition.y){
			//jump down
			Vector3 dif = _playerTransform.position - _agent.pathEndPosition;
			// get vector slightly out from platform
			dif.y = 0;
			Vector3 top = transform.position+dif.normalized*_agent.radius*5f;
			if(Physics.Raycast(top, Vector3.down, out RaycastHit hit, MAX_DROP_HEIGHT, ~LayerMask.GetMask("Enviornment"), QueryTriggerInteraction.Ignore) && NavMesh.SamplePosition(hit.point ,out NavMeshHit navMeshHit, 5f, NavMesh.AllAreas)){
				if(_link == null) _link = Instantiate(_navMeshLinkPrefab).GetComponent<NavMeshLink>();
				else _link.enabled = true;
				_link.startPoint = _agent.pathEndPosition;
				_link.endPoint = navMeshHit.position;
				_linkLifeTimer = 5f;
			}
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

	private void Attack()
    {
		_attackTimer += Random.Range(3f, 5f);
		//_attack.Attack(_targettedPosition);
	}
}