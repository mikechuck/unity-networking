using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
	UdpClientManager _udpClientManager;
	float _speed;
	void Start()
	{
		_udpClientManager = GetComponent<UdpClientManager>();
		_speed = 10;
	}
    void Update()
    {
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		Vector3 movementVector = (transform.right * horizontalInput + transform.forward * verticalInput) * _speed * Time.deltaTime;
		
		if (movementVector != Vector3.zero)
		{
			transform.position += movementVector;
			_udpClientManager.SendData(transform.position.ToString());
		}
		
	}
}
