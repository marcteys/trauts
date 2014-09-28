using UnityEngine;
using System.Collections;

public class NetworkClient : MonoBehaviour {
	
	private bool refreshing = false;
	private HostData[] hostData;
	string gameType = "Stuart";
	bool autoConnect = false;
	string defaultServerIP = "127.0.0.1";
	string serverIP = "127.0.0.1";

	// Update is called once per frame
	void Update ()
	{
		if(refreshing)
		{
			if(MasterServer.PollHostList().Length > 0)
			{
				Debug.Log(MasterServer.PollHostList().Length);
				hostData = MasterServer.PollHostList();
				refreshing = false;
				
				// If the user chose to connect automatically, we select the first game in the list
				if( autoConnect )
				{
					Network.Connect(hostData[0]);
					Debug.Log("Connecting to server");
				}
			}
		}
	}
	
	public void ConnectToServer()
	{
		Network.Connect( serverIP, 25000);
		Debug.Log("Connecting to server");
	}
	
	public void ConnectAutomaticallyToServer()
	{
		autoConnect = true;
		RefreshHostList();
	}
	
	void RefreshHostList()
	{
		Debug.Log("Refreshing host list");
		MasterServer.RequestHostList(gameType);
		refreshing = true;
	}

	void OnConnectedToServer()
	{
		Debug.Log ("Connected to server !");
	}

	void OnGUI()
	{
		if( !Network.isClient )
		{
			serverIP = GUI.TextField(new Rect(10, 10, 120, 30), serverIP);
		}
		if (GUI.Button (new Rect (10,50,60,30), "Connect"))
		{
			ConnectToServer();
		}
	}
	

}
