using UnityEngine;
using System.Collections;

public class EmcCreator : MonoBehaviour {

	public Transform emcOrigin;
	private float shadowSize = 10f;
	public float destroyAfter = 15f;
	//test
	public GameObject emcTarget;
	public Material depth;

	void Awake()
	{
		//LaunchEmc(emcOrigin,emcTarget);
	}

	public void LaunchEmc(Vector3 origin, GameObject targetObj) {

		depth = (Material)Resources.Load("Materials/DepthCutout") as Material;

		Debug.Log ("lauchEMC");

		origin = targetObj.transform.position + origin;
		targetObj = targetObj.transform.Find("emc").gameObject;


		//Debug.DrawRay(targetObj.transform.position,targetObj.transform.position + origin, Color.red,50f);



		Mesh targetMesh = targetObj.GetComponent<MeshFilter>().mesh;
		Vector3[] targetVertices = new Vector3[targetMesh.vertexCount];
		targetVertices = targetMesh.vertices;

		Vector3[] newVerticeOrigin = new Vector3[4];
		Vector3[] newVerticeDestination = new Vector3[4];

		int[] triangleNumbers = targetMesh.triangles;

		for(int t =0; t <4;t++)
		{
			if(t%3 == 0)
			{
				Vector3[] verts = new Vector3[3] { 
					targetMesh.vertices[targetMesh.triangles[t]],
					targetMesh.vertices[targetMesh.triangles[t+1]],
					targetMesh.vertices[targetMesh.triangles[t+2]],
				};

				Vector3 transformedVertice ;
				Vector3 direction;
				for(int v =0; v <verts.Length;v++)
				{
					transformedVertice = targetObj.transform.TransformPoint(verts[v]);
					direction = Vector3.Normalize(transformedVertice - origin);
					direction = direction*shadowSize;
					newVerticeOrigin[v] = transformedVertice;
					newVerticeDestination[v] = transformedVertice+direction;
				}

				for(int i =0; i <verts.Length;i++)
				{
					Vector3[] planeVertices = new Vector3[4];
					
					int j = i+1;
					if(i == 0) j=2;
					if(i == 2) j=0;

					planeVertices[0] = newVerticeOrigin[i];
					planeVertices[1] = newVerticeOrigin[j];
					planeVertices[2] = newVerticeDestination[i];
					planeVertices[3] = newVerticeDestination[j];

					GameObject plane = new GameObject("EmcPlane_"+targetObj.transform.parent.name);
					MeshFilter meshFilter = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
					meshFilter.mesh = CreateMesh(planeVertices);
					MeshRenderer renderer = plane.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
					renderer.material = depth;
					plane.transform.parent = targetObj.transform;
					plane.transform.Translate(0,0,-0.05f);
					plane.transform.tag = "SafeZone";
					Autodestroy ad = plane.AddComponent<Autodestroy>();
					ad.destroyDelay = destroyAfter;
				}
			} // end %3
		}// end triangles loop
	}



	Mesh CreateMesh(Vector3[] vertices)
	{
		Mesh mesh = new Mesh();

		mesh.vertices = vertices;
		
		int[] tri = new int[6];
		
		tri[0] = 0;
		tri[1] = 2;
		tri[2] = 1;
		
		tri[3] = 2;
		tri[4] = 3;
		tri[5] = 1;
		
		mesh.triangles = tri;
		
		Vector3[] normals = new Vector3[4];
		
		normals[0] = -Vector3.forward;
		normals[1] = -Vector3.forward;
		normals[2] = -Vector3.forward;
		normals[3] = -Vector3.forward;
		
		mesh.normals = normals;
		
		Vector2[] uv  = new Vector2[4];
		
		uv[0] = new Vector2(0, 0);
		uv[1] = new Vector2(1, 0);
		uv[2] = new Vector2(0, 1);
		uv[3] = new Vector2(1, 1);
		
		mesh.uv = uv;

		return mesh;
	}

}
