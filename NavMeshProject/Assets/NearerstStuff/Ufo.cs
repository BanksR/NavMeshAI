using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Linq;
using System.Xml.Schema;
using UnityEditorInternal;
using UnityEngine;

public class Ufo : MonoBehaviour
{

	private Transform target;
	public float speed = 1f;

	private void Awake()
	{
		target = GameManager._pickupQ.First().transform;
	}


	private void LateUpdate()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GameManager.instance.DeQ();
		}

		target = GameManager._pickupQ.First().transform;
	
		MoveToward(target.position);
		
		Debug.DrawLine(transform.position, target.position);
		MoveToward(target.position);
		
	}

	public void MoveToward(Vector3 targetPos)
	{
		Vector3 newPos = Vector3.MoveTowards(transform.position, targetPos, speed);
		
		transform.position = newPos;
	}
}
