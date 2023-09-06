using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class UdpClientManager : MonoBehaviour
{
	UdpClient _udpClient;
	IPEndPoint _remoteEndPoint;
    // Start is called before the first frame update
    void Start()
    {
        StartUDPClient("127.0.0.1", 5555);
    }

	void StartUDPClient(string ipAddress, int port)
	{
		_udpClient = new UdpClient();
		_remoteEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);

		// Start receiving data async
		_udpClient.BeginReceive(ReceiveData, null);

		//Send a message to the server
		SendData("Hello, server!");
	}

	void ReceiveData(IAsyncResult result)
	{
		byte[] receivedBytes = _udpClient.EndReceive(result, ref _remoteEndPoint);
		string receivedMessage = System.Text.Encoding.UTF8.GetString(receivedBytes);

		Debug.Log("Received from server: " + receivedMessage);

		// Continue receiving data async
		_udpClient.BeginReceive(ReceiveData, null);
	}

	public void SendData(string message)
	{
		byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(message);

		// Send message to the server
		_udpClient.Send(sendBytes, sendBytes.Length, _remoteEndPoint);

		Debug.Log("Sent to server: " + message);
	}
}
