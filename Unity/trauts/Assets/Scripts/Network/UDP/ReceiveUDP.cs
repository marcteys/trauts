using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;

public class ReceiveUDP : MonoBehaviour
{
	public int port = 2002;
	private UdpClient client;
	private IPEndPoint RemoteIpEndPoint;
	static int UDPValue;
	private Regex regexParse;
	private Thread t_udp;
	public string datas =""; 
	void Start()
	{
		client = new UdpClient(port);
		RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
		regexParse = new Regex(@"\d*$");
		t_udp = new Thread(new ThreadStart(UDPRead));
		t_udp.Name = "Mindtuner UDP thread";
		t_udp.Start();
	}
	
	public void UDPRead()
	{
		while (true)
		{
			try
			{
				Debug.Log("listening UDP port " + port);
				byte[] receiveBytes = client.Receive(ref RemoteIpEndPoint);
				string returnData = Encoding.ASCII.GetString(receiveBytes);
				// parsing
				datas = returnData;
				//Debug.Log(regexParse.Match(returnData).ToString());
				UDPValue = Int32.Parse(regexParse.Match(returnData).ToString());
			}
			catch (Exception e)
			{
				Debug.Log("Not so good " + e.ToString());
			}
			Thread.Sleep(20);
		}
	}
	
	void Update()
	{
		if (t_udp != null) Debug.Log(t_udp.IsAlive);
		Vector3 scale = transform.localScale;
		scale.x = (float)UDPValue;
	}
	
	void OnDisable()
	{
		if (t_udp != null) t_udp.Abort();
		client.Close();
	}
}
