using UnityEngine;
using UnityStandardAssets.Utility;
using System.Collections;

public class GravityScript : MonoBehaviour {

	public float speed = 5.0f;

	private Transform _player;
	private Transform _enemies;
	private Transform _sun;
	private EnemyClass[] _enemyScripts;
	private bool _flipped = false;
	private bool _done = false;

	private Quaternion _qTo = Quaternion.identity;
	private Quaternion _qToEnemy = Quaternion.identity;
	private Quaternion _qToCamera = Quaternion.identity;
	private Quaternion _qToSun = Quaternion.Euler(45, 0, 0);
	
	private Quaternion _qFlip = Quaternion.Euler(180, 0, 0);
	private Quaternion _qFlipCamera = Quaternion.Euler(0,0,180);
	private Quaternion _qFlipSun = Quaternion.Inverse(Quaternion.Euler(45, 0, 0));



	private SmoothFollow cameraFollowScript;

	// Use this for initialization
	void Start () {
		_sun = GameObject.Find("Sun").transform;
		_player = GameObject.FindGameObjectWithTag ("Player").transform;
		_enemies = GameObject.Find("Enemies").transform;
		_enemyScripts = new EnemyClass[]{};
		cameraFollowScript = Camera.main.GetComponent<SmoothFollow>();
	}

	public void Flip()
	{
		Physics.gravity = new Vector3(0, -Physics.gravity.y, 0);
		if (!_flipped){
			_qTo = _qFlip;
			_qToCamera = _qFlipCamera;
			_qToSun = _qFlipSun;
			_flipped = true;
			_enemyScripts = _enemies.GetComponentsInChildren<EnemyClass>();
			cameraFollowScript.Height -= cameraFollowScript.Height;
		}
		else {
			_qTo = Quaternion.identity;
			_qToCamera = Quaternion.identity;
			_qToSun = Quaternion.Euler(45,0,0);
			_flipped = false;
			_enemyScripts = _enemies.GetComponentsInChildren<EnemyClass>();
			cameraFollowScript.Height -= cameraFollowScript.Height;
			
		}
		
		_qToEnemy = _qTo;
		//_qTo.y = _player.rotation.y;
		_done = false;
	}

	void Update() {
		if (Input.GetButtonDown ("P1_Flip")) {
			Flip();
		}
	}

	void UpdateRotation()
	{

		_done = UpdateRotationEnemy();
		Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, _qToCamera, Time.deltaTime * speed);
		_player.rotation = Quaternion.RotateTowards(_player.rotation, _qTo, Time.deltaTime * speed);
		_sun.rotation = Quaternion.RotateTowards(_sun.rotation, _qToSun, Time.deltaTime * speed);

		_done = (_player.rotation == _qTo && Camera.main.transform.rotation == _qToCamera && _qToSun != _sun.rotation);

	}

	bool UpdateRotationEnemy()
	{
		bool done = true;
		/*
		foreach(EnemyClass e in _enemyScripts){
			if (e)
				e.transform.rotation = Quaternion.RotateTowards(e.transform.rotation, _qToEnemy, Time.deltaTime * speed);
			if (e.transform.rotation != _qToEnemy)
				done = false;
		}
		*/ // For now we just have flying enemies
		return done;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (!_done)
			UpdateRotation();
	}
}
