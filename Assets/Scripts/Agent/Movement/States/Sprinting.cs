using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinting : OnGroundState
{
    private float moveSpeed = 7f;

    public Sprinting(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Run");
        transitionsTo.Add(new Transition(typeof(Walking), Not(Run)));
        transitionsTo.Add(new Transition(typeof(Idling), Not(Move), Not(Run)));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
        transitionsTo.Add(new Transition(typeof(Sliding), Crouch));
    }

    public override void AfterExecution()
    {
        if (movement.Velocity.y > 0)
        {
            movement.SetVerticalVelocity(0);
        }
    }

    public override void BeforeExecution()
    {
        Debug.Log("Sprinting");
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        if (movement.Velocity.magnitude < moveSpeed)
        {
            newVelocity = GetAgentMovementInput();
            movement.SetHorizontalVelocity(newVelocity * moveSpeed);
            RotateAgentModelToDirection(newVelocity);
        }
        KeepGrounded();
    }

}