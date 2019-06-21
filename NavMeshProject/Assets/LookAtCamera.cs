using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour

{
	private Camera _cam;
	// Use this for initialization
	void Start ()
	
	{
		_cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()

	{
		Vector3 v = _cam.transform.position - transform.position;

		v.x = v.z = 0;

		transform.LookAt(_cam.transform.position - v);
		transform.Rotate(0, 180, 0);

	}
}
