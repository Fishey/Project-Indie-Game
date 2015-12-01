using UnityEngine;
using System.Collections;

public class RangedEnemyClass : EnemyClass {

	// Use this for initialization
	protected override void Movement()
	{
		float distanceToPlayer = Vector3.Distance (transform.position, _player.position);
		_rigidbody.AddForce(transform.forward * 250f * movementSpeed);
		
		if (_rigidbody.velocity.magnitude > 2)
		{
			_rigidbody.velocity = _rigidbody.velocity.normalized * 2f;
		}
		if (_rigidbody.velocity.magnitude < 0.1f)
		{
			_rigidbody.AddForce(transform.up * 25f);
		}
		
		if (distanceToPlayer < aggroRange)
		{
			_destination = _player.position;
			Attack();
		}
		else if (distanceToPlayer > aggroRange)
			_destination = _origin;

	}

	override protected void Turning()
	{
		if (_destination != null){
			if (Vector3.Distance(transform.position, _destination) > 0.1f)
			{
				Vector3 direction = new Vector3((_destination - transform.position).normalized.x, (_destination - transform.position).normalized.y, 0);
				Quaternion lookRotation = Quaternion.LookRotation(direction);
				if (_flip)
					lookRotation *= Quaternion.Euler(0,0,180);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 100f * Time.deltaTime);
			}
		}
	}

	public override void Attack() {
		if ( attackSpeed != 0 && _timeSinceAttack > 2f / attackSpeed){
			_timeSinceAttack = 0f;
			Debug.Log("pew");
			GameObject ball = Instantiate(Resources.Load("EnemyBall", typeof(GameObject))) as GameObject;
			Vector3 direction = new Vector3((_destination - transform.position).normalized.x, (_destination - transform.position).normalized.y, 0);
			ball.transform.position = transform.position;
			ball.GetComponent<Rigidbody>().AddForce(direction * 200f);
		}
	}
}
