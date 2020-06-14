using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    [SerializeField] private float healthGive; 
     public float GiveHealth(float currentHealth,float maxHealth)
     {
        
        currentHealth += this.healthGive;
        if (currentHealth > maxHealth)
        {
           currentHealth = maxHealth;
        }
           
        return currentHealth;
     }
}
