﻿using System.Collections;
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
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        if (movement.Velocity.magnitude < moveSpeed)
        {
            newVelocity = Vector3.zero;
            if (controller.Forwards)
            {
                newVelocity += movement.lookDirection.forward;
            }
            if (controller.Backwards)
            {
                newVelocity += -movement.lookDirection.forward;
            }
            if (controller.Left)
            {
                newVelocity += -movement.lookDirection.right;
            }
            if (controller.Right)
            {
                newVelocity += movement.lookDirection.right;
            }
            newVelocity = newVelocity.normalized;
            movement.SetHorizontalVelocity(newVelocity * moveSpeed);
            RotateAgentModelToDirection(newVelocity);
            newVelocity = newVelocity.normalized;
            movement.SetHorizontalVelocity(newVelocity * moveSpeed);
        }
        KeepGrounded();
    }

}