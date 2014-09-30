using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {

	public static Color repulsiveColor = new Color(1,0.6f,0f,1);
	public static Color attractiveColor = new Color(0.007f,0.442f,0.963f,1);
	public static Color emcColor  = new Color(0.067f,0.574f,0.071f,1);

	void Start() {
		Debug.Log (repulsiveColor);
	}
}
