using UnityEngine;
using System.Collections;

public class EmcCreator : MonoBehaviour {

	public Transform emcOrigin;

	//test
	public GameObject emcTarget;

	void Awake()
	{
		LaunchEmc(emcOrigin,emcTarget);
	}



	public void LaunchEmc(Transform origin, GameObject targetObj) {

		Mesh targetMesh = targetObj.GetComponent<MeshFilter>().mesh;
		Vector3[] targetVertices = new Vector3[targetMesh.vertexCount];
		targetVertices = targetMesh.vertices;


		for(int i=0; i < targetVertices.Length-2;i++)
		{
			Vector3 transformedVertice = targetObj.transform.TransformPoint(targetVertices[i]);
			
			Vector3 direction = Vector3.Normalize(transformedVertice - origin.position);

			//Debug.DrawLine(transformedVertice,direction*5f, Color.black,50f);

			Debug.DrawLine(transformedVertice,transformedVertice+Vector3.up, Color.red,50f);
			Debug.DrawLine(transformedVertice,origin.position,Color.green,50f);

			Debug.DrawLine(transformedVertice,direction, Color.black,50f);
		}


	}

}
