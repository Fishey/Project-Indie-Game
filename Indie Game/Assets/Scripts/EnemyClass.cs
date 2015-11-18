﻿using UnityEngine;
using System.Collections;

public class EnemyClass : Entity {

	Transform player;
	Vector3 origin;
	NavMeshAgent agent;
	public float aggroRange;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		agent = GetComponent<NavMeshAgent> ();
		origin = transform.position;
	}

	// Update is called once per frame
	void Update () {
		float distanceToPlayer = Vector3.Distance (transform.position, player.position);
		if (distanceToPlayer < aggroRange && agent.isActiveAndEnabled && agent.isOnNavMesh)
			agent.SetDestination (player.position);
		else agent.SetDestination (origin);
	}

	protected override void Die()
	{
		Destroy (gameObject);
	}
}