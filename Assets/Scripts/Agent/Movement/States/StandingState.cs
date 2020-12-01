using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class StandingState : MovementState
{
    public Func<bool> AboveWalkingSpeed => () => movement.CurrentVelocity.magnitude >= movement.walkSpeed;
    public Func<bool> StandingStill => () => movement.CurrentVelocity.magnitude <= .01f;

    protected StandingState(GameObject gameObject) : base(gameObject)
    {
    }
}
