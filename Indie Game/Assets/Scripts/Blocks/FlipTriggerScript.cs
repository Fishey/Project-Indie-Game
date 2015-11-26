using UnityEngine;
using System.Collections;

public class FlipTriggerScript : MonoBehaviour {

	private GravityScript _worldGravityScript;
	private float _coolDown = 0f;
	// Use this for initialization
	void Start () {
		_worldGravityScript = GameObject.Find("World").GetComponent<GravityScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (_coolDown > 0f)
			_coolDown -= Time.deltaTime;
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "Shuriken" && col.gameObject.layer != 11)
		{
			if (_coolDown <= 0f){
				_worldGravityScript.Flip();
				_coolDown = 2f;
			}
			col.gameObject.layer = 11;
		}


	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Weapon")
		{
			if (_coolDown <= 0f){
				_worldGravityScript.Flip();
				_coolDown = 2f;
			}
		}
	}
}
