using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyTank : MonoBehaviour


{
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

    [Header ("UI Helpers")]
    //Some simple text to help us debug the system
    public Text distanceText;
    public Text distToPlayer;

    private Transform playerPos;
    public LayerMask playerLayer;
    private bool canSee;

    // Components for the ray cast
    private Ray posToPlayer;
    private RaycastHit hit;

	// Use this for initialization
	void Start ()
    {
        // Filling our variables with component refernces
        _navMesh = GetComponent<NavMeshAgent>();
        _waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // Set our Agent on to its first waypoint - SetDestination, unsurprisingly, sets a destination...
        _navMesh.SetDestination(_waypoints[0].transform.position);
        
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Cast a ray from the enemy tank to the player every frame
        posToPlayer = new Ray(transform.position, playerPos.position - transform.position);
        float range = Vector3.Distance(transform.position, playerPos.position);

        
        if (Physics.Raycast(posToPlayer, out hit))
        {
            if (hit.collider.CompareTag("Player") && range <= ViewRange)
            {
                canSee = true;
                Target();
                Debug.DrawLine(transform.position, playerPos.position);
            }
           
            
        }
        else
        {
            canSee = false;
            Patrol();
        }

        

        
        distToPlayer.text = "Player: " + range +"\n"+ "Can See: " + canSee;

        distanceText.text = "Distance: " + _navMesh.remainingDistance.ToString();


        
        
        
        
        


        

        
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
        emit.rateOverDistance = _navMesh.speed * particleRate;
    }

    private void Patrol()
    {
        // This function can be called every frame to set the unit to loop thru 
        // its waypoints
        
        _navMesh.isStopped = false;
        
        if (_navMesh.remainingDistance < 1f)
        {
            
            _currentWP = (_currentWP + 1) % _waypoints.Length;
            _navMesh.SetDestination(_waypoints[_currentWP].transform.position);
            Debug.Log("Stopped:" + _navMesh.isStopped);
            
        }
    }

    private void Target()
    {
        // If the player object is near - stop navigation and rotate toawrds the player
        _navMesh.isStopped = true;
        transform.LookAt(playerPos.position);
    }
}
