using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyTank : MonoBehaviour


{
    private NavMeshAgent _navMesh;
    private NavMeshPath _navPath;

    public ParticleSystem exhaustParticles;
    public float particleRate = 10f;

    private GameObject[] _waypoints;
    private int _currentWP = 0;

    public Text distanceText;
    public Text distToPlayer;

    public Transform playerPos;

    private Ray posToPlayer;

	// Use this for initialization
	void Start ()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        _navMesh.SetDestination(_waypoints[0].transform.position);
        
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        posToPlayer = new Ray(transform.position, playerPos.position - transform.position);
        Debug.DrawLine(transform.position, playerPos.position);

        distToPlayer.text = "Player: " + Vector3.Distance(transform.position, playerPos.position).ToString();

        distanceText.text = "Distance: " + _navMesh.remainingDistance.ToString();

        if (_navMesh.remainingDistance < 1f)
        {
            
            _currentWP = (_currentWP + 1) % _waypoints.Length;
            _navMesh.SetDestination(_waypoints[_currentWP].transform.position);
            Debug.Log("Stopped:" + _navMesh.isStopped);
            
        }

        /*
        if (Input.GetKeyDown(KeyCode.N))
        {
            _currentWP = (_currentWP + 1) % _waypoints.Length;
            _navMesh.SetDestination(_waypoints[_currentWP].transform.position);
            Debug.Log(_currentWP);
            _navMesh.isStopped = false;
        }
        */

        if (!_navMesh.isStopped)
        {
            Exhaust();
        }
	}

    private void Exhaust()
    {
        var emit = exhaustParticles.emission;
        emit.rateOverDistance = _navMesh.speed * particleRate;
    }
}
