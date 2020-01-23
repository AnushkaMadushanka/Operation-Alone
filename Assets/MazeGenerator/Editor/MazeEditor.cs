using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MazeLoader))]
public class MazeEditor : Editor {

	public override void OnInspectorGUI(){
		base.OnInspectorGUI ();
		MazeLoader ml = (MazeLoader)target;
		if (GUILayout.Button ("Generate Maze")) {
			ml.GenerateMaze ();
		}
		if (GUILayout.Button ("Clear Maze")) {
			ml.ClearMaze ();
		}
		if (GUILayout.Button ("Generate Spawn Points")) {
			ml.GenerateSpawnPoints ();
		}
	}
}
