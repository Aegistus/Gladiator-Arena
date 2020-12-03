using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinting : OnGroundState
{
    private float moveSpeed = 8f;

    public Sprinting(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Run");
        transitionsTo.Add(new Transition(typeof(Walking), Not(Run)));
        transitionsTo.Add(new Transition(typeof(Idling), Not(Move), Not(Run)));
        transitionsTo.Add(new Transition(typeof(Jumping), Jump, OnGround));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
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
        anim.Play("Run");
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        newVelocity = Vector3.zero;
        if (controller.Forwards)
        {
            newVelocity += movement.lookDirection.forward;
        }
        newVelocity = newVelocity.normalized;
        movement.SetHorizontalVelocity(newVelocity * moveSpeed);
        KeepGrounded();
    }

}