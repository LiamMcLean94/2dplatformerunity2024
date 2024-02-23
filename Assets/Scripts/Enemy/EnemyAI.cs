using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public float speed;
    public float circleRadius;
    private Rigidbody2D EnemyRB;
    [SerializeField] private GameObject GroundCheckEnemy;
    [SerializeField] private LayerMask isGroundLayer;
    [SerializeField] private bool isGrounded;
    public bool facingRight;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        EnemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyRB.velocity = Vector2.right * speed * Time.deltaTime;
        isGrounded = Physics2D.OverlapCircle(GroundCheckEnemy.transform.position, circleRadius, isGroundLayer);
        animator.SetTrigger("Walk");
        if(!isGrounded && facingRight)
        {
            Flip();
        }
        else if(!isGrounded && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        speed = -speed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(GroundCheckEnemy.transform.position, circleRadius);
    }
}
