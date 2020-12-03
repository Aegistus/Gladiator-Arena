using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchWalking : OnGroundState
{
    public float moveSpeed = 2f;

    public CrouchWalking(GameObject gameObject) : base(gameObject)
    {
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Crouch Walking");
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        if (movement.Velocity.magnitude < moveSpeed)
        {
            newVelocity = GetAgentMovementInput();
            movement.SetHorizontalVelocity(newVelocity * moveSpeed);
        }
        KeepGrounded();
    }
}
