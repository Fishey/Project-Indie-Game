using UnityEngine;
using System.Collections;

public class HurtBlockScript : MonoBehaviour {

	PlayerClass _player;

	public float Damage = 25f;

	// Use this for initialization
	void Start () {
		_player = GameObject.Find("Player").GetComponent<PlayerClass>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "Player")
		{
			_player.ChangeHealth(-Damage);
			_player.GetComponent<Rigidbody>().AddExplosionForce(50000f, col.transform.position, 50f);

		}
	}
}
