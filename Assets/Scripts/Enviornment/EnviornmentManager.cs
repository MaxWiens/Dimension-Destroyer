using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class EnviornmentManager : MonoBehaviour {
	[SerializeField, NotNull] NavMeshSurface _navMeshSurface = default;
	private bool _shouldRebuild = false;
	[SerializeField, NotNull] private EnviornmentManagerSO _managerSO = default;

	public void RebuildNavMesh() => _shouldRebuild = true;

	private void OnEnable() {
		if(_managerSO.Manager != null){
			Debug.LogError("EnvionrmnetManager already registered in scene!");
		}
		_managerSO.Manager = this;
	}

	private void OnDisable() {
		_managerSO.Manager = null;
	}

	private void Start() {
		_navMeshSurface.BuildNavMesh();
	}

	private void FixedUpdate() {
		 if(_shouldRebuild){
			 _navMeshSurface.BuildNavMesh();
			 _shouldRebuild = false;
		 }
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnviornmentManager))]
public class Editor_EnviornmentManager : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
		if(Application.IsPlaying(target) && GUILayout.Button("Rebuild NavMesh"))
			(target as EnviornmentManager).RebuildNavMesh();
	}
}
#endif