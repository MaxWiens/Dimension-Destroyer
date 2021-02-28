using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using System;

public class PlayerMove : MonoBehaviour {
	public float MaxSlope = 80f;
	public float MaxSpeed = 5f;
	public float MoveForce = 5f;
	public float InitalJumpForce = 8f;
	public float HoldJumpForce = 18f;
	public bool IsGrounded => _groundingObjects.Count > 0;

	private Vector3 _groundNormal = Vector3.up;
	public Vector3 GroundNormal => _groundNormal;
	[SerializeField, NotNull] private Rigidbody _rigidBody;
	private List<Bag> _groundingObjects = new List<Bag>();
	private List<Bag> _wallObjects = new List<Bag>();
	private Vector3 _wallNormal = Vector3.zero;

	[SerializeField, NotNull] private InputManagerSO _inputs;

	private bool _isJumping = false;
	private float _jumpTimer = 0f;
	private float _jumpIncreaseTime = 0.5f;

	private void OnEnable() {
		_inputs.Moved += OnMove;
		_inputs.Jump += OnJump;
	}

	private void OnDisable() {
		_inputs.Moved -= OnMove;
		_inputs.Jump -= OnJump;
	}

	private void OnMove(Vector2 moveDir)
		=> _moveVec = moveDir;

	private void OnJump(bool pressed)
		=> _jumpPressed = pressed;

	private Vector2 _prevMoveDir = Vector2.zero;
	public void Move(Vector2 direction){

		if(direction.x != 0 || direction.y != 0){
			Vector2 v2Velocity = new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.z);
			bool doMove = false;
			if(IsGrounded && !_isJumping ){
				//gounded
				if(_rigidBody.velocity.sqrMagnitude < MaxSpeed*MaxSpeed)
					doMove = true;
			}else if(!IsGrounded){
				// not on ground
				if(new Vector2(_rigidBody.velocity.x,_rigidBody.velocity.z).sqrMagnitude < MaxSpeed*MaxSpeed)
					doMove = true;
			}

			if(!doMove && !_prevMoveDir.Equals(direction)){
				Vector2 md = direction - v2Velocity.normalized;
				doMove = true;

				// if(md.sqrMagnitude < 0){

				// }
			}



			if(doMove){
				Vector3 adjustedMoveVec = Vector3.zero;
				if(_groundNormal.x == 0 && _groundNormal.z == 0f){
					adjustedMoveVec = new Vector3(direction.x, 0f, direction.y);
				}else{
					Vector3 normalCrossUp = Vector3.Cross(Vector3.up,_groundNormal).normalized;
					Vector3 slopeDirection = Vector3.Cross(_groundNormal,normalCrossUp).normalized;
					float theata = Vector2.SignedAngle(direction, new Vector2(slopeDirection.x,slopeDirection.z))+90;
					Quaternion q = Quaternion.AngleAxis(theata, _groundNormal);
					adjustedMoveVec = q * normalCrossUp;
				}

				if(_wallNormal.x != 0f || _wallNormal.y != 0f ||_wallNormal.z != 0f)
					adjustedMoveVec = _wallNormal+adjustedMoveVec;
				if((IsGrounded && _isJumping) || !IsGrounded){
					_rigidBody.velocity = new Vector3(adjustedMoveVec.x*MoveForce, _rigidBody.velocity.y, adjustedMoveVec.z*MoveForce);
				}else if(IsGrounded){
					_rigidBody.velocity = adjustedMoveVec*MoveForce;
				}
				//_rigidBody.AddForce(adjustedMoveVec*MoveForce);
			}
		}else{
			if((IsGrounded && _isJumping) || !_isJumping){
				_rigidBody.velocity = new Vector3(_rigidBody.velocity.x*0.5f,_rigidBody.velocity.y,_rigidBody.velocity.z*0.5f);
			}else if(IsGrounded){
				_rigidBody.velocity = new Vector3(_rigidBody.velocity.x*0.5f,_rigidBody.velocity.y*0.5f,_rigidBody.velocity.z*0.5f);
			}
			_prevMoveDir = direction;
		}
		//Debug.DrawRay(transform.position, adjustedMoveVec, Color.cyan);
	}

	private Vector2 _moveVec = new Vector2();
	private bool _jumpPressed = false;
	private void Update() {
		//Debug.DrawRay(transform.position, _groundNormal, Color.blue);
		if(_isJumping)
			_jumpTimer += Time.deltaTime;
		//Debug.Log($"IsJumping: {_isJumping}");
		//Debug.Log($"IsGrounded: {IsGrounded}");
	}

	private bool IsBagNullPred(Bag b)=>b.Key == null;

	private void FixedUpdate() {
		if(_wallObjects.RemoveAll(IsBagNullPred) > 0){
			 if(_wallObjects.Count == 0){
				_wallNormal = Vector3.zero;
			 }else{
				_wallNormal = _wallObjects.First().Value;
			 }
		}
		if(_groundingObjects.RemoveAll(IsBagNullPred) > 0){
			if(_groundingObjects.Count == 0){
				_groundNormal = Vector3.up;
			 }else{
				_groundNormal = _groundingObjects.First().Value;
			 }
		}

		Jump();

		Vector3 camForward = Camera.main.transform.forward;
		camForward.y = 0;
		Vector3 camRight = new Vector3(-camForward.z, 0, camForward.x);

		Vector3 moveVec = (camForward * _moveVec.y + camRight * -_moveVec.x).normalized;
		Debug.DrawRay(transform.position, moveVec);

		Move(new Vector2(moveVec.x, moveVec.z));

		Debug.Log($"isJumping: {_isJumping}");
		Debug.Log($"isGrounded:{IsGrounded}");
	}

	public void Jump(){
		if(_jumpPressed){
			if(IsGrounded && !_isJumping){
				_rigidBody.AddForce(Vector3.up*InitalJumpForce, ForceMode.Impulse);
				_isJumping = true;
			}else if(_isJumping){
				if(_jumpTimer >= _jumpIncreaseTime){
					_jumpTimer = 0f;
					_isJumping = false;
				}else
					_rigidBody.AddForce(Vector3.up*HoldJumpForce);
			}
		}else{
			_isJumping = false;
			_jumpTimer = 0f;
		}
	}

	private void OnCollisionEnter(Collision other) {
		foreach(ContactPoint contactPoint in other.contacts){
			float f = Vector3.Angle(contactPoint.normal, Vector3.up);
			if(f <= MaxSlope){
				_groundNormal = contactPoint.normal;
				_groundingObjects.Add(new Bag(other.gameObject, _groundNormal));
				//Debug.Log($"new normal {f}");
				break;
			}else if(f <= 180 - MaxSlope){
				//Debug.Log($"wall normal? {f}");
				if(other.gameObject.layer == LayerMask.NameToLayer("Enviornment")){
					_wallNormal= contactPoint.normal;
					_wallObjects.Add(new Bag(other.gameObject, _wallNormal));
				}
			}else{
				//Debug.Log($"celing normal? {f}");
			}

		}
	}

	private void OnCollisionStay(Collision other) {
		if(_groundingObjects.Remove(new Bag(other.gameObject))){
			bool isGood = false;
			foreach(ContactPoint contactPoint in other.contacts){
				float f = Vector3.Angle(contactPoint.normal, Vector3.up);
				if(f <= MaxSlope){
					// is floor
					_groundNormal = contactPoint.normal;
					_groundingObjects.Add(new Bag(other.gameObject, _groundNormal));
					isGood = true;
					break;
				}else if(f <= 180 - MaxSlope){
					//Debug.Log($"wall normal? {f}");
					_wallNormal= contactPoint.normal;
					_wallObjects.Add(new Bag(other.gameObject, _wallNormal));
				}else{
					//Debug.Log($"celing normal? {f}");
				}
			}
			if(!isGood){
				_groundNormal = Vector3.up;
			}
		}
	}



	private void OnCollisionExit(Collision other) {
		if(_groundingObjects.Remove(new Bag(other.gameObject))){
			if(_groundingObjects.Count == 0)
				_groundNormal = Vector3.up;
			return;
		}else if(_wallObjects.Remove(new Bag(other.gameObject))){
			if(_wallObjects.Count == 0)
				_wallNormal = Vector3.zero;
			return;
		}
	}

	private struct Bag{
		private static object nullkeybag = new object();
		public GameObject Key;
		public Vector3 Value;

		public Bag(GameObject key){
			Key = key;
			Value = Vector3.zero;
		}
		public Bag(GameObject key, Vector3 value){
			Key = key;
			Value = value;
		}

		public override bool Equals(object obj)
		{
			return obj is Bag b && b.Key.Equals(Key);
		}

		public override int GetHashCode()
		{
			return Key == null ? nullkeybag.GetHashCode() : Key.GetHashCode();
		}
	}
}