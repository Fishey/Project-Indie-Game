using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

	private float _damage = 25;
	private float _attackSpeed = 1;

	private float _coolDownTimer = 0;

	// Use this for initialization
	void Start () {
	
	}

	public float Damage
	{
		get { return this._damage;}
		set { this._damage = value;}
	}

	public float AttackSpeed
	{
		get { return this._attackSpeed;}
		set { this._attackSpeed = value;}
	}
	
	// Update is called once per frame
	void Update () {
		if (_coolDownTimer > 0)
			_coolDownTimer -= Time.deltaTime;
	}

	void OnCollisionEnter(Collision collision) 
	{
		if (collision.collider.tag == "Enemy" && _coolDownTimer <= 0) {
			Debug.Log ("hit enemy");

			collision.gameObject.GetComponent<EnemyClass> ().ChangeHealth (-Damage);
			if (AttackSpeed != 0)
				_coolDownTimer = 1/AttackSpeed;

		}
	}
}
