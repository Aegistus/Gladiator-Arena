﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idling : OnGroundState
{

    public Idling(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Idle");
        transitionsTo.Add(new Transition(typeof(Walking), MoveKeys));
        transitionsTo.Add(new Transition(typeof(Jumping), Spacebar, Not(Falling), Not(Rising)));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));

        //transitionsTo.Add(new Transition(typeof(TakingDamage), () => Input.GetKeyDown(KeyCode.Q)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        //anim.Play(animationNames[0]);
        if (movement.Velocity.magnitude < 10)
        {
            movement.SetHorizontalVelocity(Vector3.zero);
        }
        movement.SetVerticalVelocity(0);
        KeepGrounded();
        //if (OnGround())
        //{
        //    rb.velocity = Vector3.zero;
        //}
    }

    public override void DuringExecution()
    {

    }
}