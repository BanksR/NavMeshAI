using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

	public static ParticleManager instance;
	public Vector3 spawnOffset;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

		else
		{
			Destroy(this.gameObject);
		}
	}


	public void PlayThese(GameObject go, Transform spawnPos)
	{
		Vector3 spawn = new Vector3(spawnPos.transform.position.x, .5f, spawnPos.transform.position.z);
		Instantiate(go, spawn, Quaternion.identity);
	}
}
