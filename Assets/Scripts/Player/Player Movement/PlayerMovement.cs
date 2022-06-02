using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Collider2D ladderGround;

    [SerializeField] float walkSpeed = 300f;
    [SerializeField] float jumpPower = 1700f;
    [SerializeField] float climbSpeed = 300f;

    float xMovement;
    float yMovement;

    bool atLadder = false;
    bool isClimbing = false;

    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        Climb();
        
        if(!isClimbing)
        {
            Walk();
            Jump();
        }


        rb.velocity = new(xMovement, yMovement);
    }

    void Walk()
    {
        xMovement = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime;
    }

    void Jump()
    {
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            yMovement = Input.GetAxis("Jump") * jumpPower * Time.deltaTime;
        }
        else
        {
            yMovement = rb.velocity.y;
        }
    }

    void Climb()
    {
        if(atLadder)
        {
            if (Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Epsilon)
            {
                yMovement = Input.GetAxis("Vertical") * Time.deltaTime * climbSpeed;
                isClimbing = true;
            }
            else

            {
                yMovement = 0f;
                isClimbing = false;
            }

        }
        else
        {
            yMovement = rb.velocity.y;
        }

        if(isClimbing)
        {
            xMovement = 0f;
            ladderGround.isTrigger = true; 
        }
        else
        {
            xMovement = rb.velocity.x;
            ladderGround.isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        atLadder = collision.gameObject.CompareTag("Ladder") || collision.gameObject.CompareTag("Ladder Ground");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        atLadder = !collision.gameObject.CompareTag("Ladder") || collision.gameObject.CompareTag("Ladder Ground");
    }
}
