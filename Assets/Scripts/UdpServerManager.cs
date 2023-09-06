using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class UdpServerManager : MonoBehaviour
{
	UdpClient _udpServer;
	IPEndPoint _remoteEndPoint;
    // Start is called before the first frame update
    void Start()
    {
        StartUDPServer(5555);
    }

	void StartUDPServer(int port)
	{
		_udpServer = new UdpClient(port);
		_remoteEndPoint = new IPEndPoint(IPAddress.Any, port);

		// Start receiving data async
		_udpServer.BeginReceive(ReceiveData, null);
	}

	void ReceiveData(IAsyncResult result)
	{
		byte[] receivedBytes = _udpServer.EndReceive(result, ref _remoteEndPoint);
		string receivedMessage = System.Text.Encoding.UTF8.GetString(receivedBytes);

		Debug.Log("Received from client: " + receivedMessage);

		// Continue receiving data async
		_udpServer.BeginReceive(ReceiveData, null);
	}

	void SendData(string message, IPEndPoint endPoint)
	{
		byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(message);

		// Send message to the server
		_udpServer.Send(sendBytes, sendBytes.Length, endPoint);

		Debug.Log("Sent to server: " + message);
	}
}