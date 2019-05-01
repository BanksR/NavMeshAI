using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Projectile : MonoBehaviour
{

	public Rigidbody _rbd;

	public IDamageable pct;

	public ParticleSystem impactParts;

	public float Damage { get; set; }

	public float _speed;

	
	private void OnEnable()
	{
		if (pct == null)
		{
			//Debug.Log("Firer not found!");
		}
	}

	// Use this for initialization
	void Awake ()
	{
		//pct = GetComponentInParent<IDamageable>();

		//Damage = pct.ShootDamage();
		
		_rbd = GetComponent<Rigidbody>();



		_rbd.AddForce(transform.forward * _speed, ForceMode.Impulse);
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.GetComponent<IDamageable>() != null)
		{
			Instantiate(impactParts, other.contacts[0].point, Quaternion.identity);
			
			IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
			
			damageable.TakeDamage(Damage);

			//Debug.Log(damageable);
			
			StartCoroutine(Kill());
		}

		else
		{
			StartCoroutine(Kill());
		}

	}

	IEnumerator Kill()
	{
		float t = 0f;
		while (t < 2f)
		{
			t += Time.fixedDeltaTime;
			yield return new WaitForFixedUpdate();
		}

		this.gameObject.SetActive(false);
		
		yield return null;
	}

}
