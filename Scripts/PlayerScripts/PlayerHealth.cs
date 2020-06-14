using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    private float maxHealth = 100;
    [SerializeField] private Text healthDisplay;
    [SerializeField] private float currentHealth;
    [SerializeField] private GameObject deathUi;

    void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    { 
        healthDisplay.text = "Health: " + this.currentHealth.ToString() + "%";
        if(currentHealth<=0)
        {
            deathUi.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Destroy(this.gameObject);
        }
    }
    public float GetPlayerHealth()
    {
        return currentHealth;
    }
    public void SetPlayerHealth(float health)
    {
        currentHealth = health;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    public void GetDamage(float amount)
    {
        currentHealth -= amount;
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
