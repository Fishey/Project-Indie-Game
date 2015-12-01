using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class ShurikenScript : WeaponScript {

	private bool hit = false;
	private float lifeTime = 10.0f;
	public float Damage = 20;
	private PlayerClass _player;

	// Use this for initialization
	void Start () {
		_player = GameObject.Find("Player").GetComponent<PlayerClass>();

	}
	
	// Update is called once per frame
	void Update () {
		if (!hit) transform.Rotate(new Vector3(0,9,0));
		else {
			transform.Rotate(new Vector3(9,0,0));
		}
		lifeTime -= Time.deltaTime;
		if (lifeTime < 0f)
		{
			_player.Shurikens++;
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "Enemy")
		{
			if (!hit)
				col.collider.GetComponent<EnemyClass>().ChangeHealth(-20);
			hit = true;
			gameObject.layer = 11;
		}

		if (col.collider.tag == "Player" && gameObject.layer == 11)
		{
			FMOD_StudioSystem.instance.PlayOneShot("event:/ShurikenPickup", transform.position);
			_player.Shurikens++;
			Destroy(gameObject);
		}

		if (col.collider.tag == "Untagged")
		{
			gameObject.layer = 11;
			hit = true;
		}

		if (col.collider.tag == "Flip")
		{
			hit = true;
		}

	}
}
