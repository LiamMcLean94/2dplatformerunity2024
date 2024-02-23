using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]

public class Soul : MonoBehaviour
{
    public enum SoulType
    {
        BlueSoul,
        JumpPotion
    }

    [SerializeField] SoulType currentSoulType;

    Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            
            switch (currentSoulType)
            {
                case SoulType.JumpPotion:
                    pc.StartJumpForcePotion();
                    Destroy(gameObject);
                    break;
            }
            
            

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player detected");

            PlayerController pc = collision.GetComponent<PlayerController>();
            
            if (/*pc != null && */ Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E key pressed");
                
                if(currentSoulType == SoulType.BlueSoul)
                {
                    Debug.Log("Animator set to collect");
                    anim.SetTrigger("Collect");
                  
                    StartCoroutine(DestroyAfterAnimation());
                       
                }
            }
        }
    }

    IEnumerator DestroyAfterAnimation()
    {
        Debug.Log("DestroyAfterAnimation coroutine started");
       
        yield return null;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
