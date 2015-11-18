﻿using UnityEngine;
using System.Collections;

public class GravityScript : MonoBehaviour {

	Transform player;
	public float speed = 5.0f;
	private bool flipped = false;
	private Quaternion qTo = Quaternion.identity;
	private Quaternion qToCamera = Quaternion.identity;
	private Quaternion qForward = Quaternion.identity;
	private Quaternion qFlip = Quaternion.Euler(180, 0, 0);
	private Quaternion qFlipCamera = Quaternion.Euler(0,0,180);

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("P1_Fire1")) {
			Physics.gravity = new Vector3(0, -Physics.gravity.y, 0);
			if (!flipped){
				qTo = qFlip;
				qToCamera = qFlipCamera;
				flipped = true;
			}
			else {
				qTo = Quaternion.identity;
				qToCamera = Quaternion.identity;
				flipped = false;
			}
		}
		Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, qToCamera, Time.deltaTime * speed);
		player.rotation = Quaternion.RotateTowards(player.rotation, qTo, Time.deltaTime * speed);
	}
}
