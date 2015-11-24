using UnityEngine;
using System.Collections;

public class EnemyClass : Entity {

	private Transform _player;
	private Vector3 _origin;
	private Vector3 _destination;

	private Rigidbody _rigidbody;
	private float destroyTimer = 10f;
	public float Speed;
	private float _distToGround;
	private Transform _mainCam;
	private MeshCollider _collider;

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
	}

	void Movement()
	{
		float distanceToPlayer = Vector3.Distance (transform.position, _player.position);
		_rigidbody.AddForce(transform.forward * 500f);

		if (distanceToPlayer < aggroRange)
		{
			Vector3 movement = Vector3.right;
			_destination = _player.position;
		}
		else if (distanceToPlayer > aggroRange)
			_destination = _origin;

	}

	void Turning()
	{
		if (_destination != null){
			if (Vector3.Distance(transform.position, _destination) > 0.1f)
			{
				Vector3 direction = new Vector3((_destination - transform.position).normalized.x, 0, 0);
				Quaternion lookRotation = Quaternion.LookRotation(direction);
				
				transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 100f * Time.deltaTime);
			}
		}
	}

	void CheckPos()
	{
		if (transform.position.y < -100f)
		{
			destroyTimer -= Time.deltaTime;
		}

		if (destroyTimer < 0f)
		{
			Die ();
		}
	}

	protected override void Die()
	{
		Destroy (gameObject);
	}
}
