using UnityEngine;
using System.Collections;

public class EnemyClass : Entity {

	protected Transform _player;
	protected Vector3 _origin;
	protected Vector3 _destination;

	protected Rigidbody _rigidbody;
	protected float _destroyTimer = 10f;
	protected float _distToGround;
	private float _timeSinceAttack;
	protected Transform _mainCam;
	protected MeshCollider _collider;
	public float Damage;

	public float aggroRange;
	
	void Start()
	{
		_player = GameObject.FindGameObjectWithTag ("Player").transform;
		_origin = transform.position;
		_collider = GetComponent<MeshCollider> ();
		_rigidbody = GetComponent<Rigidbody>();
		_mainCam = Camera.main.transform;
		_distToGround = _collider.bounds.extents.y;
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
		float distanceToPlayer = Vector3.Distance (transform.position, _player.position);
		_rigidbody.AddForce(transform.forward * 500f);

		if (distanceToPlayer < aggroRange)
		{
			_destination = _player.position;
		}
		else if (distanceToPlayer > aggroRange)
			_destination = _origin;

	}

	protected virtual void Turning()
	{
		if (_destination != null){
			if (Vector3.Distance(transform.position, _destination) > 0.1f)
			{
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

	protected override void Die()
	{
		Destroy (gameObject);
	}
}
