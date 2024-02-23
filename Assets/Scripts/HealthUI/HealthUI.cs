using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public GameObject heartPrefab;
    public Transform heartsParent;
    public float maxHealth = 100f; // Adjust as needed
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Example method to update health value
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        // Clear existing hearts
        foreach (Transform child in heartsParent)
        {
            Destroy(child.gameObject);
        }

        // Instantiate hearts based on current health
        float heartCount = Mathf.Ceil(currentHealth / 10f); // Each heart represents 10% health
        for (int i = 0; i < heartCount; i++)
        {
            Instantiate(heartPrefab, heartsParent);
        }
    }
    
   
}
