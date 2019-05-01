using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticleControl : MonoBehaviour
{

	private ParticleSystem[] _parts;

	private void Awake()
	{
		_parts = GetComponentsInChildren<ParticleSystem>();

		foreach (var p in _parts)
		{
			p.Play();
			StartCoroutine(KillParticles());
		}

	}

	IEnumerator KillParticles()
	{
		float t = 0;

		while (t < 10f)
		{
			t += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		
		this.gameObject.SetActive(false);
		yield return null;
	}
}
