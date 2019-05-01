using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyTank : MonoBehaviour, IDamageable


{
    [Header("Tank Stats")] 
    // Reference to our flyweight design pattern data set
    public TankerStats _stats;

    public Material _mat;

    public Text nameText;
    public Slider HP;
    public Slider cooldown;
    
    
    [Header ("Nav Mesh")]
    // A refence to the NavMeshAgant component on the gameobject
    private NavMeshAgent _navMesh;
    

    [Header ("Particle Stuff")]
    // Particle stuff to make it look fancy
    public ParticleSystem exhaustParticles;
    public float particleRate = 10f;

    [Header("Targetting")] 
    public float ViewRange = 5f;

    // An array of waypoint prefab gameobjects
    private GameObject[] _waypoints;
    private int _currentWP = 0;

    public GameObject ExplosionParticles;

    [Header ("UI Helpers")]
    //Some simple text to help us debug the system
    //public Text distanceText;
    //public Text distToPlayer;

    private Transform playerPos;
    public LayerMask playerLayer;
    public bool canSee;

    // Components for the ray cast
    private Ray posToPlayer;
    private RaycastHit hit;
    
    // Damage
    private float currentHP;

	// Use this for initialization
	void Start ()
    {
        // Filling our variables with component refernces
        _navMesh = GetComponent<NavMeshAgent>();
        _mat.color = _stats.skinColor;
        _waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        canSee = false;

        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // Set our Agent on to its first waypoint - SetDestination, unsurprisingly, sets a destination...
        _navMesh.SetDestination(_waypoints[0].transform.position);
        _navMesh.speed *= _stats.speedMulti;

        currentHP = _stats.maxHp;
        nameText.text = _stats.tankerName;
        HP.value = _stats.maxHp / _stats.maxHp;

    }
	
	// Update is called once per frame
	void Update ()
    {
        //Cast a ray from the enemy tank to the player every frame
        posToPlayer = new Ray(transform.position, playerPos.position - transform.position);
        float range = Vector3.Distance(transform.position, playerPos.position);


        if (Physics.Raycast(posToPlayer, out hit, playerLayer))
        {
            if (hit.collider.CompareTag("Player"))
            {
                
                Target();

            }
            else if (!hit.collider.CompareTag("Player") || hit.collider == null)
            {

                Patrol();
            }
           
            
        }
        Debug.DrawLine(transform.position, playerPos.position);





        //distToPlayer.text = "Player: " + range + "\n" + "Can See: " + hit.collider.tag;

        //distanceText.text = "Distance: " + _navMesh.remainingDistance.ToString();


        // If the Agent is moving - play the particle system
        if (!_navMesh.isStopped)
        {
            Exhaust();
        }
	}
    
    
    // This function deals with particles, please ignore...
    private void Exhaust()
    {
        var emit = exhaustParticles.emission;

        if (_navMesh.velocity.sqrMagnitude > 0.1f)
        {
            emit.rateOverDistance = _navMesh.velocity.sqrMagnitude * particleRate;
        }
        else
        {
            emit.rateOverDistance = 0f;
        }

        
    }


    private void Patrol()
    {
        // This function can be called every frame to set the unit to loop thru 
        // its waypoints

        canSee = false;
        _navMesh.isStopped = false;

        
        if (_navMesh.remainingDistance < 1f)
        {
            
            _currentWP = (_currentWP + 1) % _waypoints.Length;
            _navMesh.SetDestination(_waypoints[_currentWP].transform.position);

            
        }
    }

    private void Target()
    {
        // If the player object is near - stop navigation and rotate toawrds the player
        canSee = true;
        _navMesh.isStopped = true;

        transform.LookAt(playerPos.position);
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP > 0)
        {
            HP.value = currentHP / _stats.maxHp;
        }

        else
        {
            Kill();
        }
    }

    public float ShootDamage()
    {
        return _stats.weaponDmg;
    }

    public void Kill()
    {
        ParticleManager.instance.PlayThese(ExplosionParticles, this.transform);
        this.gameObject.SetActive(false);
    }
}
