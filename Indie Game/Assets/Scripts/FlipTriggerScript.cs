using UnityEngine;
using System.Collections;

public class FlipTriggerScript : MonoBehaviour {

	private GravityScript _worldGravityScript;
	private bool _coolDown;
	// Use this for initialization
	void Start () {
		_worldGravityScript = GameObject.Find("World").GetComponent<GravityScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		Debug.Log(col.gameObject.layer);

		if (col.collider.tag == "Shuriken" && col.gameObject.layer != 11)
		{
			Debug.Log("ye");
			_worldGravityScript.Flip();
			col.gameObject.layer = 11;
		}

	}
}
