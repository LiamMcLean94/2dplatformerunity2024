using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionTimerUI : MonoBehaviour
{
    private Text timerText;
    public float totalTime = 10f; // Adjust as needed
    private float timeLeft;

    void Start()
    {
        // Find the Text component with the name "potionTimer" in children
        timerText = transform.Find("potionTimer").GetComponent<Text>();
        if (timerText == null)
        {
            Debug.LogError("Text component with name 'potionTimer' not found for timer in children!");
        }

        timeLeft = totalTime;
        UpdateTimerUI();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        UpdateTimerUI();

        if (timeLeft <= 0)
        {
            // Handle collectible expiration
            Destroy(gameObject);
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time Left: " + Mathf.RoundToInt(timeLeft);
        }
    }
}
