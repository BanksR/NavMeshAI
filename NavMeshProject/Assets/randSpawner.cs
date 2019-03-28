using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randSpawner : MonoBehaviour
{


	public GameObject thingToSpawn;


	public float spawnEvery = 3f;
	public float radius = 4;

	private float _timer = 0;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		_timer += Time.deltaTime;
		if (_timer >= spawnEvery)
		{
			_timer = 0f;
			Spawn();
		}
	}

	public void Spawn()
	{
		Vector3 pos = Random.insideUnitCircle * radius;
		Instantiate(thingToSpawn, pos, Quaternion.identity);
		
		GameManager.instance.AddAndResort(thingToSpawn);
	}
}
