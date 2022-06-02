using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : MonoBehaviour
{
    [SerializeField] float walkSpeed = 10f;
    [SerializeField] float jumpPower = 10f;
    [SerializeField] float climbSpeed = 5f;

    public float WalkSpeed { get { return walkSpeed; } private set { walkSpeed = value; } }
    public float JumpPower { get { return jumpPower; } private set { jumpPower = value; } }
    public float ClimbSpeed { get { return climbSpeed; } private set { climbSpeed = value; } }

    public Rigidbody2D Rigidbody { get; set; }
    public Animator Animator { get; private set; }


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }


    private IPlayerMovementState currentState = new PlayerGroundedIdle();
    
    void Walk()
    {
        currentState.Walk(this);
    }
    void Jump()
    {
        currentState.Jump(this);
    }    
    void Idle()
    {
        currentState.GroundedIdle(this);
    }
    void InAir()
    {
        currentState.InAir(this);
    }

    public void SetState(IPlayerMovementState newState)
    {
        currentState = newState;
    }

    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            Walk();
        }
        else if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        else if (!Rigidbody.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            InAir();
        }
        else
        {
            Idle();
        }
    }

    void FixedUpdate()
    {
        
    }
}
