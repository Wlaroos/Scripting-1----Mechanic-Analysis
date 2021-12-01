using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	Vector3 moveJump = Vector2.zero;
	float horMove, vertMove;
	void Start()
	{
		SheetAssigner SA = FindObjectOfType<SheetAssigner>();
		Vector2 tempJump = SA.roomDimensions + SA.gutterSize;

		//Distance between rooms is used for amount of movement
		moveJump = new Vector3(tempJump.x, tempJump.y, 0); 
	}
	void Update()
	{
		if (Input.GetKeyDown("w") || Input.GetKeyDown("s") || Input.GetKeyDown("a") || Input.GetKeyDown("d")) 
		{
			//Capture input
			horMove = System.Math.Sign(Input.GetAxisRaw("Horizontal"));
			vertMove = System.Math.Sign(Input.GetAxisRaw("Vertical"));
			Vector3 tempPos = transform.position;
			//Teleport based on input
			tempPos += Vector3.right * horMove * moveJump.x;
			tempPos += Vector3.up * vertMove * moveJump.y;

			transform.position = tempPos;
		}
	}
}