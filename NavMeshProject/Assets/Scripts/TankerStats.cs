using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "Tanker Stats")]

public class TankerStats : ScriptableObject
{

	public string tankerName = "Name";
	public float maxHp = 10f;
	public int weaponDmg = 5;

	public float speedMulti = 1f;

	public Color skinColor;
}
