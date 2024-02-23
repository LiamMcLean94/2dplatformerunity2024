using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class Enemy : MonoBehaviour
{

    public bool TestMode = false;
    
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int currentHealth;

    Rigidbody2D rb;
    public Animator animator;

    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();

        
    }


    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        Debug.Log("Enemy is hurt");
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy Died");
        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
