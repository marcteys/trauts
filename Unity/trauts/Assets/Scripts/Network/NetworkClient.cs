using UnityEngine;
using System.Collections;

public class NetworkClient : MonoBehaviour {
	
	private bool refreshing = false;
	private HostData[] hostData;
	string gameType = "Stuart";
	bool autoConnect = false;
	string defaultServerIP = "10.1.4.49";
	string serverIP = "10.1.4.49";
	

	
	// Update is called once per frame
	void Update () {
		if(refreshing){
			if(MasterServer.PollHostList().Length > 0){
				Debug.Log(MasterServer.PollHostList().Length);
				hostData = MasterServer.PollHostList();
				refreshing = false;
				
				// If the user chose tp connect automatically, we select the first game in the list
				if( autoConnect ){
					Network.Connect(hostData[0]);
					Debug.Log("Connecting to server");
					HideConnectGUI();
				}
			}
		}
	}
	
	public void ConnectToServer(){
		Network.Connect( serverIP, 25000);
		Debug.Log("Connecting to server");
		HideConnectGUI();
	}
	
	public void ConnectAutomaticallyToServer(){
		autoConnect = true;
		RefreshHostList();
	}
	
	void RefreshHostList(){
		Debug.Log("Refreshing host list");
		MasterServer.RequestHostList(gameType);
		refreshing = true;
	}
	
	void OnGUI(){
		if( !Network.isClient ){
			serverIP = GUI.TextField(new Rect(10, 10, 150, 50), serverIP);
		}
	}
	
	void HideConnectGUI(){
		GameObject.Find("ConnectGUI").active = false;
	}
}
