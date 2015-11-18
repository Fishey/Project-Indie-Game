using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public enum ABILITIES { first, second, third }
public enum GunType { Support, Damage, Tank }

[RequireComponent(typeof(PlayerMovement))]
//[RequireComponent(typeof(ShieldAbility))]
public class PlayerClass : Entity
{
    [SerializeField]
    List<GameObject> ownedWeapons = new List<GameObject>();
    int gunIndex;
    PlayerMovement playerMove;
    [SerializeField]
    Vector3 lastCheckpoint;
    [SerializeField]
    Transform currentWeapon;
    float utime = 0;
    float dTime = 0;


    //only in player
    int money = 0;
    [SerializeField]
    int xp = 0;
    float xptoNextlvl = 100;
    [SerializeField]
    int level = 1;
    [SerializeField]
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
			if (xp >= xptoNextlvl)
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

    public float GetAttackSpeed()
    {
        float tmp = attackSpeed;
        return tmp;
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
        xptoNextlvl *= 1.5f;
    }
    //End Stats

    public void saveCheckpoint(Vector3 Checkpoint)
    {
        lastCheckpoint = Checkpoint;
    }
  
    protected override void Die()
    {
        playerMove.SetMovementSpeed(0);
        SetStats(0, attackSpeed * 2);
    }
    void Start()
    {
        //currentWeapon = GetComponentInChildren<SupportGunScript>();

        playerMove = GetComponent<PlayerMovement>();
        playerMove.SetOwner(this);
        playerMove.SetMovementSpeed(movementSpeed);
    }
    public float GetEnergy() // PlayerMovement needs this for abilities
    {
        return currentEnergy;
    }
}
