using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour, IDamageable
{

	public TankerStats _stats;
	
    public float rotateSpeed = 2f;
    public float speed = 10f;
    public ParticleSystem _exhaustParts;
    public float particleAmount = 30f;

	public Text nameField;
	public Slider HP;

	public int wepDamage;
	

    private Rigidbody _rbd;
	private FireControl _fireControl;
	private float currentHP;

	// Use this for initialization
	void Start ()
    {
        _rbd = GetComponent<Rigidbody>();
	    _fireControl = GetComponent<FireControl>();
	    wepDamage = _stats.weaponDmg;

	    nameField.text = _stats.tankerName;
	    HP.value = _stats.maxHp / _stats.maxHp;
	    currentHP = _stats.maxHp;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

	    if (Input.GetMouseButtonDown(0) && _fireControl.canShoot)
	    {
		    _fireControl.StartShooting();
	    }

	    Move(horizontal, vertical);

        var emit = _exhaustParts.emission;
        emit.rateOverDistance = Mathf.Abs(vertical) * particleAmount;
	}

    private void Move(float h, float v)
    {
        _rbd.AddForce(transform.forward * v * speed);
        transform.Rotate(Vector3.up * h * rotateSpeed);

    }

	public void TakeDamage(float damage)
	{
		currentHP -= damage;
		HP.value = currentHP / _stats.maxHp;
		Debug.Log("Ouch: "+ name + " has taken: " +damage + " damage..." + currentHP / _stats.maxHp);
	}

	public float ShootDamage()
	{
		return _stats.weaponDmg;
	}
}
