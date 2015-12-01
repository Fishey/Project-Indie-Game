using UnityEngine;
using UnityStandardAssets.Utility;
using System.Collections;

public class GravityScript : MonoBehaviour {

	public float speed = 5.0f;

	private Transform _player;
	private Transform _enemies;
	private EnemyClass[] _flyingEnemyScripts;
	private bool _flipped = false;
	public bool Done = false;
	public bool Debugging = true;

	private Quaternion _qTo = Quaternion.identity;
	private Quaternion _qToEnemy = Quaternion.identity;
	private Quaternion _qToCamera = Quaternion.identity;

	private Quaternion _qFlip = Quaternion.Euler(180, 0, 0);
	private Quaternion _qFlipCamera = Quaternion.Euler(0,0,180);



	private SmoothFollow cameraFollowScript;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag ("Player").transform;
		_enemies = GameObject.Find("Enemies").transform;
		_flyingEnemyScripts = new EnemyClass[]{};
		cameraFollowScript = Camera.main.GetComponent<SmoothFollow>();
	}

	public void Flip()
	{
		_enemies = GameObject.Find("Enemies").transform;
		Physics.gravity = new Vector3(0, -Physics.gravity.y, 0);
		if (!_flipped){
			FMOD_StudioSystem.instance.PlayOneShot("event:/GravitySwitcherUp", transform.position);

			_qTo = _qFlip;
			_qToCamera = _qFlipCamera;
			_flipped = true;
			_flyingEnemyScripts = _enemies.GetComponentsInChildren<EnemyClass>();
			cameraFollowScript.Height -= cameraFollowScript.Height;
		}
		else {
			FMOD_StudioSystem.instance.PlayOneShot("event:/GravitySwitcherDown", transform.position);

			_qTo = Quaternion.identity;
			_qToCamera = Quaternion.identity;
			_flipped = false;
			_flyingEnemyScripts = _enemies.GetComponentsInChildren<EnemyClass>();
			cameraFollowScript.Height -= cameraFollowScript.Height;
			
		}
		
		_qToEnemy = _qTo;
		//_qTo.y = _player.rotation.y;
		Done = false;
		UpdateRotationEnemy();
	}

	void Update() {
		if (Input.GetButtonDown ("P1_Flip")) {
			Flip();
		}
	}

	void UpdateRotation()
	{

		Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, _qToCamera, Time.deltaTime * speed);
		_player.rotation = Quaternion.RotateTowards(_player.rotation, _qTo, Time.deltaTime * speed);
		//_sun.rotation = Quaternion.RotateTowards(_sun.rotation, _qToSun, Time.deltaTime * speed);

		if (Debugging)
		Debug.Log(_player.rotation.eulerAngles + " <<PLAYER TARGET>> " + _qTo.eulerAngles + " Done = " + (_player.rotation == _qTo));

		Done = (_player.rotation.eulerAngles == _qTo.eulerAngles);

	}

	void UpdateRotationEnemy()
	{
		foreach(EnemyClass e in _flyingEnemyScripts){
			e.Flipped = _flipped;
		}
		 // For now we just have flying enemies
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (!Done)
			UpdateRotation();
	}
}
