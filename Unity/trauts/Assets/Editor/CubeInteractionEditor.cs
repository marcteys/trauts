using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CubeInteraction))]
public class CubeInteractionEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		CubeInteraction myScript = (CubeInteraction)target;
		if(GUILayout.Button("Change Type Right"))
		{
			myScript.SwipteType(true);
		}
		if(GUILayout.Button("Change Type Left"))
		{
			myScript.SwipteType(false);
		}

		if(GUILayout.Button("Create WAve"))
		{
			myScript.CreateWave();
		}
	}
}