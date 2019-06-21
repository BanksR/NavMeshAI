using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireControl : MonoBehaviour
{
    public GameObject projectile;
    
    public Transform LaunchPos;


    private float _fireRate = 3f;


    public float FireRate
    {
        get { return _fireRate; }
        
        set { _fireRate = value; }
    }
    
    public Slider coolDown;

    public bool canShoot = true;
    public float ShootDamage = 0f;

    public ParticleSystem[] _boomParticles;

    

    protected virtual void  Awake()
    {
        canShoot = true;
        
    }
    

    public void StartShooting()
    {
        StartCoroutine(Shoot());
    }

    public IEnumerator Shoot()
    {
        float t = 0;
        GameObject go = Instantiate(projectile, LaunchPos.position, LaunchPos.rotation);
        Projectile proj = go.GetComponent<Projectile>();
        proj.Damage = ShootDamage;
        

        foreach (var p in _boomParticles)
        {
            p.Play();
        }

        while (t < _fireRate)
        {
            canShoot = false;
            t += Time.fixedDeltaTime;
            coolDown.value = t / _fireRate;
            
            yield return new WaitForEndOfFrame();
        }

        canShoot = true;
        yield return null;
    }
}
