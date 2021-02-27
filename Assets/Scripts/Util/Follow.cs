using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
	public bool SnapToTargetOnEnable = true;
	[Min(0)] public float SnappingDistance = 0.01f;
	[Min(0)] public float Strength = 2f;
	public Transform Target;
	private Vector3 _velocity = Vector3.zero;

	private void OnEnable() {
		if(SnapToTargetOnEnable && Target != null){
			transform.position = Target.position;
		}
	}

	private void LateUpdate() {
		if(Target != null && transform.position != Target.position){
			Vector3 dif = transform.position - Target.position;
			if(Vector3.Distance(transform.position, Target.position) <= SnappingDistance){
				transform.position = Target.position;
			}else{
				transform.position += dif * Mathf.Clamp01(Strength*Time.deltaTime);// Vector3.SmoothDamp(transform.position, Target.position, ref _velocity, DampenTime);
			}
		}
	}
}