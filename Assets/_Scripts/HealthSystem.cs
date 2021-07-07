using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem 
{
    public Action OnDamaged;
    public Action OnHealed;
    public Action OnDeath;

    public const int maxHealth = 4;

    private List<HealthUnit> healthList;

    public HealthSystem(int healthUnitsAmount)
    {
        healthList = new List<HealthUnit>();
        for (int i = 0; i< healthUnitsAmount; i++)
        {
            HealthUnit healthUnit = new HealthUnit(true);
            healthList.Add(healthUnit);
        }

        //healthList[healthList.Count - 1].SetHealth(false);
        //Damage(6);
    }
    
    public void Damage(int damageAmount)
    {

        for(int i = healthList.Count -1; i>= 0; i--)
        {
            if(damageAmount > 0)
            {
                HealthUnit healthUnit = healthList[i];
                if (healthUnit.GetHealth())
                {
                    healthUnit.Damage();
                    damageAmount--;
                }
            }            
        }
        if(OnDamaged != null) OnDamaged.Invoke();
        if(DeathCheck())
        {
            if (OnDeath != null) OnDeath.Invoke();
        }
    }

    public void Heal(int healAmount)
    {
        for(int i = 0; i<healthList.Count; i++)
        {
            if (healAmount > 0)
            {
                HealthUnit healthUnit = healthList[i];
                if (!healthUnit.GetHealth())
                {
                    healthUnit.Heal();
                    healAmount--;
                }
            }
        }
        if (OnHealed != null) OnHealed.Invoke();
    }

    private bool DeathCheck()
    {
        return healthList[0].GetHealth();
    }

    public List<HealthUnit> GetHealthList()
    {
        return healthList;
    }

    public class HealthUnit
    {
        private bool isFull;

        public HealthUnit(bool isFull)
        {
            this.isFull = isFull;
        }

        public bool GetHealth()
        {
            return isFull;
        }

        public void SetHealth(bool isFull)
        {
            this.isFull = isFull;
        }
        
        public void Damage()
        {
            if(isFull)
            {
                isFull = false;
            }
        }

        public void Heal()
        {
            if(!isFull)
            {
                isFull = true;
            }
        }
    }

}
