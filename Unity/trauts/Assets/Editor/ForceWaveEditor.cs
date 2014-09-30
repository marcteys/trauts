using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ForceWave))]
public class ForceWaveEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		ForceWave myScript = (ForceWave)target;
		if(GUILayout.Button("Create Wave"))
		{
			myScript.CreateWave();
		}
	}
}