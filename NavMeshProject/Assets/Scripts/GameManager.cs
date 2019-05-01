using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Linq;
using System.Xml;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public static GameManager instance;
	public static Transform target;
	

	public delegate void ColorChanger();

	public static event ColorChanger colChange;

	public Color newCol = Color.black;

	//public static List<pickUpObject> _pickUpObjects = new List<pickUpObject>();

	public static Queue<pickUpObject> _pickupQ = new Queue<pickUpObject>();

	public GameObject _ufo;
	public static int numofCollectibles;
	

	void Awake()
	{
		
		// Create our Singleton Pattern instance logic
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
		
		//numofCollectibles = _pickUpObjects.Count;
		Debug.Log(_pickupQ.Count);
	}
	
	

	
	
	
/*
	public Transform SortList(Vector3 pos)
	{

		if (_pickUpObjects.Count != 0)
		{
			target = _pickUpObjects.OrderBy(x => Vector3.Distance(x.transform.position, pos)).First().transform;
			
		}
		
		else target = this.transform;

		return target;


		
		Debug.Log("Target: " + target.name);
	}
	*/
	

	
	
	// Spawns the particle puff when collected - ignore me...
	public void ParticleSpawner(GameObject go, Vector3 pos)
	{
		Instantiate(go, pos, Quaternion.identity);
	}

	public void DeQ()
	{
		_pickupQ.Dequeue();
		
		Debug.Log("Q Len: "+ _pickupQ.Count + "::" +_pickupQ.First().name );
		
	}

}

