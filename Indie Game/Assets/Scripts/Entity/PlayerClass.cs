using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerClass : Entity
{
  	public List<WeaponScript> OwnedWeapons = new List<WeaponScript>();
   	public WeaponScript CurrentWeapon;
	public HudScript Hud;

	private PlayerMovement _playerMove;
  	private int _money = 0;
	private int _xp = 0;
	public float XpToNextlvl = 100;

	private int _shurikens = 5;
	private int _maxShurikens = 5;
	private int _level = 1;
	private int _score = 0;
	private int _damage = 20;

	private Vector3 _lastCheckpoint;

	void Start()
	{
		_maxShurikens = _shurikens;
		OwnedWeapons.Add (transform.GetComponentInChildren<WeaponScript> ());
		CurrentWeapon = OwnedWeapons [0];
		CurrentWeapon.Damage = 25;
		_playerMove = GetComponent<PlayerMovement>();
		_playerMove.SetOwner(this);
		_playerMove.SetMovementSpeed(movementSpeed);
		saveCheckpoint(transform.position);
		Hud = GameObject.Find("Hud").GetComponent<HudScript>();
		WorldGravity = GameObject.Find("World").GetComponent<GravityScript>();
	}
	

	
    public override void ChangeEnergy(float Modifier)
    {
        base.ChangeEnergy(Modifier);
		Hud.ChangeEnergy(maxEnergy, currentEnergy);

    }

    public override void ChangeHealth(float Modifier)
    {
        base.ChangeHealth(Modifier);
		Hud.ChangeHealth(maxHealth, currentHealth);
		if (Alive && Modifier < 0)
		FMOD_StudioSystem.instance.PlayOneShot("event:/PlayerGetHit", transform.position);

    }

	public override void Attack(){
		if (!CurrentWeapon.transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("SwingTest")){
			FMOD_StudioSystem.instance.PlayOneShot("event:/SwordSwing", transform.position);
			CurrentWeapon.transform.parent.GetComponent<Animator>().Play("SwingTest");
		}
	}

	public int Xp // Amount of experience player has
	{
		get { return this._xp;}
		set {
			this._xp = value;
			if (_xp >= XpToNextlvl)
				levelUp ();
		}
	}

    public int MovementSpeed //Speed at which a player moves
	{
		get { return this.movementSpeed;}
		set { 
			this.movementSpeed = value;
			_playerMove.SetMovementSpeed(value);
		}
	}

    public float AttackSpeed {
		get { return this.attackSpeed;}
		set { this.attackSpeed = value;
			if (CurrentWeapon != null) CurrentWeapon.AttackSpeed = value;
		}

	}

	public int Score
	{
		get { return this._score;}
		set { this._score += value;}
	}
	
	public int Money
	{
		get { return this._money;}
		set { this._money += value;}
	}
	
	public int Shurikens{
		get {return _shurikens;}
		set {
			if (value > _maxShurikens)
				_shurikens = _maxShurikens;
			else if (value < 0)
				_shurikens = 0;
			else 
				this._shurikens = value;
			
			Hud.ChangeShurikens(_maxShurikens, _shurikens);
		}
	}

    public void SetStats(int moveSpeed, float attackSpeed)
    {
        _playerMove.SetMovementSpeed(moveSpeed);
		this.attackSpeed = attackSpeed;
    }
    public void ResetStats()
    {
        _playerMove.SetMovementSpeed(movementSpeed);
		ChangeHealth(maxHealth-currentHealth);
		ChangeEnergy(maxEnergy-currentEnergy);
		Shurikens = _maxShurikens;
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		GetComponent<Rigidbody>().isKinematic = false;
    }

    void FixedUpdate()
    {
		if (Alive)
        Regen(4f);

    }

    void levelUp()
    {
        _level++;
        _xp = 0;
        attackSpeed += 0.05f;
        maxHealth += 10;
        maxEnergy += 20;
		CurrentWeapon.Damage+= 10;
        ResetStats();
        XpToNextlvl *= 1.5f;
    }

    public void saveCheckpoint(Vector3 Checkpoint)
    {
        _lastCheckpoint = Checkpoint;
    }
  
    protected override void Die()
    {
		if (Alive){
			FMOD_StudioSystem.instance.PlayOneShot("event:/PlayerDeath", transform.position);
			Alive = false;
			StartCoroutine("Respawn");
		}
    }

	private IEnumerator Respawn()
	{
		yield return new WaitForSeconds(5f);
		Alive = true;
		ResetStats();
		GameObject.Find("World").GetComponent<LevelBuilder>().RespawnEnemies();
		transform.position = _lastCheckpoint;
	}

    
	public float Energy {
		get {return this.currentEnergy;}
		set {this.currentEnergy = value;
		}
	}
}
