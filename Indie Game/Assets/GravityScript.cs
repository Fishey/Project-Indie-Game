using UnityEngine;
using System.Collections;

public class GravityScript : MonoBehaviour {

	Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Physics.gravity = new Vector3(0, -Physics.gravity.y, 0);
			Camera.main.transform.Rotate(new Vector3(0,0,180));
			player.Rotate(new Vector3(180.0f, 0, 0));
		}
	}
}
