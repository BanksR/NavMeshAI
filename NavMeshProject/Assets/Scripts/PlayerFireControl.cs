using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireControl : FireControl
{
	private PlayerController _playerController;
	
	protected override void Awake()
	{
		base.Awake();
		_playerController = GetComponent<PlayerController>();
		//Projectile proj = projectile.GetComponent<Projectile>();
		ShootDamage = _playerController._stats.weaponDmg;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
