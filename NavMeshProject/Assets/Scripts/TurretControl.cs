using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{

	public GameObject turret;

	public Camera cam;
	public float turnSpeed = 1f;

	private void Awake()
	{
		cam = Camera.main;
	}

	private void Update()
	{

		Turning();

	}

	void Turning()
	{
		Ray camray = cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(camray, out hit, 1000f))
		{
			
			//Vector3 rotTarget = hit.point - turret.transform.position;
			//Vector3 rotTarget = Vector3.Lerp(turret.transform.forward, hit.point - turret.transform.position,
			//	Time.deltaTime * turnSpeed);
			Vector3 rotTarget = Vector3.Lerp(turret.transform.forward, hit.point - turret.transform.position,
				Time.deltaTime * turnSpeed);
			
			rotTarget.y = 0f;
			Quaternion turretRot = Quaternion.LookRotation(rotTarget);
			turret.transform.rotation = turretRot;
		}
	}
}
