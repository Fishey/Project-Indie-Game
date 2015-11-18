using UnityEngine;
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

    public virtual void ChangeHealth(float Modifier)
    {
        currentHealth += Modifier;
       
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
    public Vector3 GetPosition()
    {
        Vector3 tmp = transform.position;
        return tmp;
    }
    protected virtual void Die()
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

    protected virtual void Regen()
    {
        time = time + Time.deltaTime;
        if (time >= 2)
        {
            regenerateHealth();
            regenerateEnergy();
            time = 0;
        }
      
    }

}
