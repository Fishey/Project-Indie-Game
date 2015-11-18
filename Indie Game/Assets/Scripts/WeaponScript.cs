using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) 
	{
		Debug.Log ("hit something");
		if (collision.collider.tag == "Enemy")
			Destroy(collision.gameObject);
	}
}
