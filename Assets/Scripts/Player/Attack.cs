using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public int damageAmount = 10;
    //public Collider2D attackCollider;

    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    //private int flipDirection = 1;
    public bool facingRight = true;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                PlayerAttack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        
    }

    private void PlayerAttack()
    {
        //play an attack animnation
        animator.SetTrigger("Attack");

        
        
        //detect enemies in range
        Collider2D[] hitEnemiesForward = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        

        //dame them
        foreach(Collider2D enemy in hitEnemiesForward)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damageAmount);
        }

        // Detect enemies behind the player
        Vector2 oppositeDirection = -transform.right; // Opposite direction from player's facing direction
        Vector2 oppositeAttackPoint = (Vector2)attackPoint.position + oppositeDirection * attackRange;
        Collider2D[] hitEnemiesOpposite = Physics2D.OverlapCircleAll(oppositeAttackPoint, attackRange, enemyLayers);

        // Damage enemies behind the player
        foreach (Collider2D enemy in hitEnemiesOpposite)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damageAmount);
        }

    }

    /*private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        
        //transform.Rotate(new Vector3(0, 180, 0));
    }*/

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    /*private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0);
    }*/
    
}
