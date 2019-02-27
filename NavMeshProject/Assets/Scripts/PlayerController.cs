using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float rotateSpeed = 2f;
    public float speed = 10f;
    public ParticleSystem _exhaustParts;
    public float particleAmount = 30f;

    private Rigidbody _rbd;

	// Use this for initialization
	void Start ()
    {
        _rbd = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Move(horizontal, vertical);

        var emit = _exhaustParts.emission;
        emit.rateOverDistance = Mathf.Abs(vertical) * particleAmount;
	}

    private void Move(float h, float v)
    {
        _rbd.AddForce(transform.forward * v * speed);
        transform.Rotate(Vector3.up * h * rotateSpeed);

    }
}
