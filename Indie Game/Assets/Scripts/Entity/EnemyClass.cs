using UnityEngine;
using System.Collections;

public class EnemyClass : Entity {

	protected Transform _player;
	protected Vector3 _origin;
	protected Vector3 _destination;

	protected Rigidbody _rigidbody;
	protected float _destroyTimer = 10f;
	protected float _distToGround;
	protected float _timeSinceAttack;
	protected Transform _mainCam;
	protected MeshCollider _collider;
	public float Damage;

	public float aggroRange;
	[SerializeField]
	protected bool _flip = false;
	
	public bool Flipped
	{
		get {return this._flip;}
		set {this._flip = value;}
	}

	
	void Start()
	{
		_player = GameObject.FindGameObjectWithTag ("Player").transform;
		_origin = transform.position;
		_collider = GetComponent<MeshCollider> ();
		_rigidbody = GetComponent<Rigidbody>();
		_mainCam = Camera.main.transform;
		_distToGround = _collider.bounds.extents.y;
		WorldGravity = GameObject.Find("World").GetComponent<GravityScript>();
	}

	public override void ChangeHealth(float Modifier)
	{
		base.ChangeHealth(Modifier);
		if (Modifier < 0)
			FMOD_StudioSystem.instance.PlayOneShot("event:/EnemyHit", transform.position);
	}

	private bool isGrounded()
	{
		bool canJump = false;
		if (Physics.Raycast (transform.position, Vector3.up, _distToGround + 0.1f) || Physics.Raycast (transform.position, -Vector3.up, _distToGround + 0.1f))
			canJump = true;
		return canJump;
	}

	// Update is called once per frame
	void Update () {
		Turning();
		Movement();
		CheckPos();
		_timeSinceAttack+= Time.deltaTime;
	}

	protected virtual void Movement()
	{
		// Determine distance to player
		float distanceToPlayer = Vector3.Distance (transform.position, _player.position);
		
		// Move forward with a given speed
		_rigidbody.AddForce(transform.forward * 500f);

		// If player is within range of sight, move towards them
		if (distanceToPlayer < aggroRange)
		{
			_destination = _player.position;
		}
		// If not in range, move around original spawn location
		else if (distanceToPlayer > aggroRange)
			_destination = _origin;

	}

	protected virtual void Turning()
	{
		if (_destination != null){
			// If Destination is known, and we aren't directly on it right now...
			if (Vector3.Distance(transform.position, _destination) > 0.1f)
			{
				// ... we turn towards our destination.
				Vector3 direction = new Vector3((_destination - transform.position).normalized.x, 0, 0);
				Quaternion lookRotation = Quaternion.LookRotation(direction);
				
				transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 100f * Time.deltaTime * movementSpeed);
			}
		}
	}

	void CheckPos()
	{
		if (transform.position.y < -1000f)
		{
			_destroyTimer -= Time.deltaTime;
		}

		if (_destroyTimer < 0f)
		{
			Die ();
		}
	}

	public override void Attack ()
	{
		if ( attackSpeed != 0 && _timeSinceAttack > 2f / attackSpeed){
			_timeSinceAttack = 0f;
			_player.GetComponent<PlayerClass>().ChangeHealth(-Damage);
		}

	}

	protected void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "Player"){
			Attack();
		}
	}

	protected void OnCollisionStay(Collision col)
	{
		if (col.collider.tag == "Player"){
			Attack();
		}
	}

	protected override void Die()
	{
		if (Alive){
			//FMOD_StudioSystem.instance.PlayOneShot("event:/EnemyDeath", transform.position);
			Alive = false;
			gameObject.SetActive(false);
		}

	}

	public void Respawn()
	{
		currentHealth = maxHealth;
		transform.position = _origin;
		gameObject.SetActive(true);
	}
}
