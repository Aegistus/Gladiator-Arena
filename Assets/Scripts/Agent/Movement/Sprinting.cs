﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinting : MovementState
{
    private float moveSpeed = 15f;

    public Sprinting(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Run");
        transitionsTo.Add(new Transition(typeof(Walking), Not(Shift)));
        transitionsTo.Add(new Transition(typeof(Idling), Not(MoveKeys), Not(Shift)));
        transitionsTo.Add(new Transition(typeof(Jumping), Spacebar, OnGround));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Sprinting");
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        newVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            newVelocity += transform.forward;
        }
        newVelocity = newVelocity.normalized;
        movement.SetVelocity(newVelocity * moveSpeed);
        KeepGrounded();
    }

    RaycastHit rayHit;
    private void KeepGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out rayHit, 1.5f, groundLayer))
        {
            transform.position = rayHit.point + Vector3.up * .5f;
        }
    }

}