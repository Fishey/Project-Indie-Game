using UnityEngine;
using System.Collections;

public class FlyingEnemyClass : EnemyClass {

	override protected void Movement()
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

	override protected void Turning()
	{
		if (_destination != null){
			if (Vector3.Distance(transform.position, _destination) > 0.1f)
			{
				Vector3 direction = new Vector3((_destination - transform.position).normalized.x, (_destination - transform.position).normalized.y, 0);
				Quaternion lookRotation = Quaternion.LookRotation(direction);

				transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 100f * Time.deltaTime);
			}
		}
	}
}
