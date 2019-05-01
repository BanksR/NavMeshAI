using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class EnemyFireControl : FireControl
{

	private EnemyTank _enemyTank;
	


	 protected override void Awake()
	 {
		 base.Awake();
		_enemyTank = GetComponent<EnemyTank>();
		//Projectile proj = projectile.GetComponent<Projectile>();
		ShootDamage = _enemyTank._stats.weaponDmg;

	}


	private void Update()
	{
		if (_enemyTank.canSee && canShoot)
		{
			StartShooting();
		}
	}
}
