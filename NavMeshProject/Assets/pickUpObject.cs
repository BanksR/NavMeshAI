using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpObject : MonoBehaviour
{
	public GameObject particles;

	void Awake()
	{
		GameManager._pickUpObjects.Add(this);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("UFO"))
		{
			//Ufo.targID++;
			GameManager.instance.RemoveAndResort(this);
			this.gameObject.SetActive(false);
			
			
			GameManager.instance.ParticleSpawner(particles, this.transform.position);
		}
	}
}
