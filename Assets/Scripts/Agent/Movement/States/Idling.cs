using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idling : OnGroundState
{

    public Idling(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Idle");
        transitionsTo.Add(new Transition(typeof(Walking), Move));
        transitionsTo.Add(new Transition(typeof(Jumping), Jump, Not(Falling), Not(Rising)));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        if (movement.Velocity.magnitude < 10)
        {
            movement.SetHorizontalVelocity(Vector3.zero);
        }
        movement.SetVerticalVelocity(0);
        KeepGrounded();
    }

    public override void DuringExecution()
    {

    }
}
