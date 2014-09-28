using UnityEngine;
using System.Collections;

public class ApplicationTargetFramerate : MonoBehaviour {

	public int value = 60;

	void Awake()
	{
		Application.targetFrameRate = value;
	}

}
