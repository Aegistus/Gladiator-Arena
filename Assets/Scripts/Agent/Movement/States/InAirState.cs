using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InAirState : MovementState
{
    protected readonly float gravity = -9.8f;
    protected float downVelocity = 0;

    protected InAirState(GameObject gameObject) : base(gameObject)
    {

    }

    public override void BeforeExecution()
    {
        downVelocity = 0;
    }

    public override void DuringExecution()
    {
        AddDownwardVelocity();
    }

    private void AddDownwardVelocity()
    {
        downVelocity += gravity * Time.deltaTime;
        charController.SimpleMove(Vector3.down * downVelocity);
    }
}
