using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class WallScript : MonoBehaviour, IColorable
{
	private Renderer[] _mat;
	
	public Color wallCol = Color.gray;
	
	// Use this for initialization
	void Start ()
	{
		_mat = GetComponentsInChildren<Renderer>();
		GameManager.colChange += HelloWorld;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void HelloWorld()
	{
		Debug.Log("Hello from: " + this.name.ToString());
	}

	public void ChangeColor(Color col)
	{
		//Debug.Log("Hello from: "+ this.name.ToString());


		foreach (Renderer _matCol in _mat)
		{
			_matCol.material.color = col;
		}

		
	}
}
