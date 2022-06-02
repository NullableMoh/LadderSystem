using UnityEngine;

public interface IPlayerMovementState
{
    void InAir(PlayerMovementStateMachine pSM);
    void Walk(PlayerMovementStateMachine pSM);
    void Jump(PlayerMovementStateMachine pSM);
    void GroundedIdle(PlayerMovementStateMachine pSM);
}


public class PlayerGroundedIdle : IPlayerMovementState
{
    public void InAir(PlayerMovementStateMachine pSM)
    {
        Debug.Log("Fell while Idle and Grounded");
        pSM.Rigidbody.velocity = new(pSM.Rigidbody.velocity.x, pSM.Rigidbody.velocity.y);
        pSM.SetState(new PlayerInAir());
    }

    public void Jump(PlayerMovementStateMachine pSM)
    {
        Debug.Log("Jumped of Ground");
        pSM.Rigidbody.velocity = new(pSM.Rigidbody.velocity.x, pSM.JumpPower);
        pSM.SetState(new PlayerInAir());
    }

    public void Walk(PlayerMovementStateMachine pSM)
    {
        Debug.Log("Walked on ground");
        pSM.Rigidbody.velocity = new(pSM.WalkSpeed * Input.GetAxis("Horizontal"), pSM.Rigidbody.velocity.y);
        pSM.SetState(new PlayerWalking());
    }

    public void GroundedIdle(PlayerMovementStateMachine pSM)
    {
    }
}

public class PlayerWalking : IPlayerMovementState
{
    public void InAir(PlayerMovementStateMachine pSM)
    {
        Debug.Log("Walked off something");
        pSM.Rigidbody.velocity = new(pSM.WalkSpeed * Input.GetAxis("Horizontal"), pSM.Rigidbody.velocity.y);
        pSM.SetState(new PlayerInAir());
    }

    public void Jump(PlayerMovementStateMachine pSM)
    {
        Debug.Log("Jumped while walking");
        pSM.Rigidbody.velocity = new(pSM.Rigidbody.velocity.x, pSM.JumpPower);
        pSM.SetState(new PlayerInAir());
    }

    public void Walk(PlayerMovementStateMachine pSM)
    {
    }

    public void GroundedIdle(PlayerMovementStateMachine pSM)
    {
        Debug.Log("Stopped walking");
        pSM.Rigidbody.velocity = new(0f, 0f);
        pSM.SetState(new PlayerGroundedIdle());
    }
}

public class PlayerInAir : IPlayerMovementState
{
    

    public void InAir(PlayerMovementStateMachine pSM)
    {
    }

    public void Jump(PlayerMovementStateMachine pSM)
    {
    }

    public void Walk(PlayerMovementStateMachine pSM)
    {
    }

    public void GroundedIdle(PlayerMovementStateMachine pSM)
    {
        if (pSM.Rigidbody.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Debug.Log("touched ground");
            pSM.Rigidbody.velocity = new(0f, pSM.Rigidbody.velocity.y);
            pSM.SetState(new PlayerGroundedIdle());
        }
    }
}
