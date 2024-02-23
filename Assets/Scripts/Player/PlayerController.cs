using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class PlayerController : MonoBehaviour
{

    public bool TestMode = false;


    //components
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    //Movement Var
    [SerializeField] private float speed = 7.0f;
    [SerializeField] private float jumpForce = 5.0f;
    //[SerializeField] private float attackForce = 7.0f;
    [SerializeField] private float airControlMultiplier = 2f; //to apply air movement

    //Groundcheck stuff
    [SerializeField] private bool isGrounded; //every tick
    [SerializeField] private Transform GroundCheck; //attriubeted in the inspector mostly
    [SerializeField] private LayerMask isGroundLayer;
    [SerializeField] private float groundCheckRadius = 0.02f;


    public KeyCode collectSoul = KeyCode.E;
    Coroutine jumpForcePotion;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        

        if (speed <= 0)
        {
            speed = 7.0f;
            if (TestMode) Debug.Log("Speed has been set to a default value of 7.0f" + gameObject.name);
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
            if (TestMode) Debug.Log("Hey our ground check radius was defaulted to 0.02f" + gameObject.name);
        }

        if (GroundCheck == null)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "GroundCheck";
            GroundCheck = obj.transform;
            if (TestMode) Debug.Log("GroundCheck Object is created" + gameObject.name);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical"); //vertical can do crouch and stuff, this not in use right now. Jan 23

        // Update isGrounded first
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, isGroundLayer);

        if (isGrounded)
        {
            rb.gravityScale = 1;

            // Apply movement only if grounded
            rb.velocity = new Vector2(xInput * speed, rb.velocity.y);

            // Handle jumping only when grounded
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            }
            /*if(Input.GetButtonDown("Fire1"))
            {
                
                anim.SetTrigger("Attack");
                
            } */
            if(Input.GetKeyDown(collectSoul))
            {
                Debug.Log("Collect key pressed");
            }

        }
        else
        {
            rb.gravityScale = 1; // Make sure gravity is applied when not grounded
        }

       


        anim.SetBool("IsGrounded", isGrounded);
        anim.SetFloat("Speed", Mathf.Abs(xInput));

        if (xInput != 0) sr.flipX = (xInput < 0);
    }
    public void StartJumpForcePotion()
    {
        if (jumpForcePotion == null)
            jumpForcePotion = StartCoroutine(JumpForcePotion());
        else
        {
            StopCoroutine(jumpForcePotion);
            jumpForcePotion = null;
            jumpForce -= 3;
            jumpForcePotion = StartCoroutine(JumpForcePotion());
        }
    }
    
    IEnumerator JumpForcePotion()
    {
        jumpForce += 3;
        yield return new WaitForSeconds(10.0f);
        jumpForce -= 3;
        jumpForcePotion = null;
    }

    void FixedUpdate()
    {
        if(!isGrounded)
        {
            float xInput = Input.GetAxisRaw("Horizontal");
            Vector2 airControlMovement = new Vector2(xInput * airControlMultiplier, 0f) * speed * Time.fixedDeltaTime;

            //air control movement
            rb.velocity += airControlMovement;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Death"))
        {
            Destroy(gameObject);
        }
    }

}
