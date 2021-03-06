﻿using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour
{

    protected float time;

    [SerializeField]
    protected int movementSpeed = 1000;

    [SerializeField]
    protected float attackSpeed = 1;

    [SerializeField]
    protected float currentHealth = 100;

    [SerializeField]
    protected float maxHealth = 100;

    protected float maxEnergy = 100;

    [SerializeField]
    protected float currentEnergy = 100;

	public bool Alive = true;


	public GravityScript WorldGravity;

    public virtual void ChangeEnergy(float Modifier)
    {
        currentEnergy += Modifier;
        
        if (currentEnergy >= maxEnergy)
        {
            currentEnergy = maxEnergy;
        }

		if (currentEnergy <= 0)
		{
			currentEnergy = 0;
		}
    }

	protected void _knockBack()
	{
		Transform mainCam = transform;
		Vector3 pos = transform.position - new Vector3(0, 2 * mainCam.up.y, 0) + new Vector3(2 * mainCam.right.x, 0, 0);
		GetComponent<Rigidbody>().AddExplosionForce(25000f, pos, 25f);
	}

    public virtual void ChangeHealth(float Modifier)
    {
		if (Alive){
	        currentHealth += Modifier;
	       
			if (Modifier < 0 && GetComponent<ParticleSystem>()){
				_knockBack();
				GetComponent<ParticleSystem>().Play();

			}

	        if (currentHealth >= maxHealth)
	        {
	            currentHealth = maxHealth;
	        }
	        if (currentHealth <= 0)
	        {
	            currentHealth = 0;
	            Die();
	        }
		}
    }

    public float Health
    {
        get
        {
            return currentHealth;
        }
		set
		{
			this.currentHealth = value;

			if (currentHealth >= maxHealth)
			{
				currentHealth = maxHealth;
			}

			if (currentHealth <= 0)
			{
				currentHealth = 0;
				Die();
			}
		}
    }

    protected virtual void Die()
    {
    }

	public virtual void Attack()
	{
	}


    // Update is called once per frame
    void regenerateHealth()
    {
        ChangeHealth(1);
    }
    void regenerateEnergy()
    {
        ChangeEnergy(2.5f);
    }

    protected virtual void Regen(float speed)
    {
        time = time + Time.deltaTime;
        if (time >= 2 / speed)
        {
            regenerateEnergy();
            time = 0;
        }
      
    }

}
