using UnityEngine;
using System.Collections;

public class EmcCreator : MonoBehaviour {

	public Transform emcOrigin;
	private float shadowSize = 10f;
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

		Vector3[] newVerticeOrigin = new Vector3[4];
		Vector3[] newVerticeDestination = new Vector3[4];

		int[] triangleNumbers = targetMesh.triangles;
		Debug.Log ("tl = " + targetMesh.triangles.Length);

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
					direction = Vector3.Normalize(transformedVertice - origin.position);
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

					
					GameObject plane = new GameObject("Plane");
					MeshFilter meshFilter = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
					meshFilter.mesh = CreateMesh(planeVertices);
					MeshRenderer renderer = plane.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
					renderer.material.shader = Shader.Find ("Particles/Additive");
				}

			}

		}

	
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
