using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpObject : MonoBehaviour
{
	public GameObject particles;

	void Awake()
	{

		GameManager._pickupQ.Enqueue(this);
	}
	
	

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("UFO"))
		{
			
			//GameManager.instance.DeQ();
			
			this.gameObject.SetActive(false);
			
			//GameManager.instance.ParticleSpawner(particles, this.transform.position);
		}
	}
}
