using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Linq;
using System.Xml.Schema;
using UnityEditorInternal;
using UnityEngine;

public class Ufo : MonoBehaviour
{

	private Transform target = null;
	public float speed = 1f;
	public static int targID = 0;

	private int targetID = 0;
	

	private void Update()
	{


		target = GameManager.instance.SortList(this.transform.position);
		
		
			MoveToward(target.position);
		
			Debug.DrawLine(transform.position, target.position);
			MoveToward(target.position);
			//Debug.Log("Target is: " + target.name);
	}

	public void MoveToward(Vector3 targetPos)
	{
		Vector3 newPso = Vector3.MoveTowards(transform.position, targetPos, speed);
		transform.position = newPso;
	}
}
