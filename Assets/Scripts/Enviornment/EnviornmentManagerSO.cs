using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "EnviornmentManager", menuName = "GameJam/EnviornmentManager", order = 0)]
public class EnviornmentManagerSO : ScriptableObject {
	public EnviornmentManager Manager;
	public void RebuildNavMesh() => Manager?.RebuildNavMesh();
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnviornmentManagerSO))]
public class Editor_EnviornmentManagerSO : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
		EnviornmentManagerSO so = target as EnviornmentManagerSO;
		if(Application.IsPlaying(target) && so.Manager != null && GUILayout.Button("Rebuild NavMesh"))
			(target as EnviornmentManagerSO).RebuildNavMesh();
	}
}
#endif