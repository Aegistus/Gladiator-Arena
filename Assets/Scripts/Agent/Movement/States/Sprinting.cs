using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinting : OnGroundState
{
    private float moveSpeed = 6f;

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
        if (movement.velocity.y > 0)
        {
            movement.AddVerticalVelocity(0);
        }
    }

    public override void BeforeExecution()
    {
        Debug.Log("Sprinting");
    }

    Vector3 inputVelocity;
    public override void DuringExecution()
    {
        inputVelocity = GetAgentMovementInput();
        movement.SetHorizontalVelocity(inputVelocity * moveSpeed);
        RotateAgentModelToDirection(inputVelocity);
        KeepGrounded();
    }

}