using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerClass : Entity
{
    [SerializeField]
    List<WeaponScript> ownedWeapons = new List<WeaponScript>();
    PlayerMovement playerMove;
    [SerializeField]
    Vector3 lastCheckpoint;
    [SerializeField]
    WeaponScript currentWeapon;

    //only in player
    int money = 0;
    [SerializeField]
    int xp = 0;
    public float xpToNextlvl = 100;
    int level = 1;
    int score = 0;

   public int Score
	{
		get { return this.score;}
		set { this.score += value;}
	}

	public int Money
	{
		get { return this.money;}
		set { this.money += value;}
	}

    public override void ChangeEnergy(float Modifier)
    {
        base.ChangeEnergy(Modifier);
    }

    public override void ChangeHealth(float Modifier)
    {
        base.ChangeHealth(Modifier);
    }

	public void AddXp(int expAdded)
	{
		Xp += expAdded;
	}

	public int Xp // Amount of experience player has
	{
		get { return this.xp;}
		set {
			this.xp = value;
			if (xp >= xpToNextlvl)
				levelUp ();
		}
	}

    public int MovementSpeed //Speed at which a player moves
	{
		get { return this.movementSpeed;}
		set { 
			this.movementSpeed = value;
			playerMove.SetMovementSpeed(value);
		}
	}

    public float AttackSpeed {
		get { return this.attackSpeed;}
		set { this.attackSpeed = value;
			if (currentWeapon != null) currentWeapon.AttackSpeed = value;
		}

	}

    public void SetStats(int moveSpeed, float attackSpeed)
    {
        playerMove.SetMovementSpeed(moveSpeed);
		this.attackSpeed = attackSpeed;
    }
    public void ResetStats()
    {
        playerMove.SetMovementSpeed(movementSpeed);
    }

    void FixedUpdate()
    {
        //Regen();

    }

    public void SwitchWeapon()
    {
        
    }

    void levelUp()
    {
        level++;
        xp = 0;
        attackSpeed += 0.05f;
        maxHealth += 5;
        maxEnergy += 1;
        ResetStats();
        xpToNextlvl *= 1.5f;
    }
    //End Stats

    public void saveCheckpoint(Vector3 Checkpoint)
    {
        lastCheckpoint = Checkpoint;
    }
  
    protected override void Die()
    {
		transform.position = lastCheckpoint;
		currentHealth = maxHealth;
    }
    void Start()
    {
		ownedWeapons.Add (transform.GetComponentInChildren<WeaponScript> ());
		currentWeapon = ownedWeapons [0];
		currentWeapon.Damage = 25;
        playerMove = GetComponent<PlayerMovement>();
        playerMove.SetOwner(this);
        playerMove.SetMovementSpeed(movementSpeed);
    }
    public float GetEnergy() // PlayerMovement needs this for abilities
    {
        return currentEnergy;
    }
}
