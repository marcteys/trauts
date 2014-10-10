using UnityEngine;
using System.Collections;

public class NetworkServer : MonoBehaviour {
	
	string gameType = "Stuart";
	
	// Use this for initialization
	void Start () {
		StartServer2();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void StartServer(){
		Debug.Log("Starting Server");
		Debug.Log ("Server has public address : "+Network.HavePublicAddress()+". Will use NAT PunchThrough : "+!Network.HavePublicAddress());
		Network.InitializeServer(32, 25000, !Network.HavePublicAddress());
		Debug.Log("Registering on MasterServer");
		MasterServer.RegisterHost(gameType, "MultiDraw", "Agencement d'espace");
	}
	
	void StartServer2(){
		Debug.Log("Starting Server");
		Network.InitializeServer(32, 25000, false);
		Debug.Log("No registration on MasterServer");
	}
	
	void OnServerInitialized(){
		Debug.Log("Server initialized !");
	}
}
