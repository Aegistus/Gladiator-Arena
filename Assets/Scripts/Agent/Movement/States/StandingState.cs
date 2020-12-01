using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class StandingState : MovementState
{
    public Func<bool> AtWalkingSpeed => () => movement.CurrentVelocity.magnitude >= movement.walkSpeed;
    public Func<bool> AtRunningSpeed => () => movement.CurrentVelocity.magnitude >= movement.runSpeed;

    protected StandingState(GameObject gameObject) : base(gameObject)
    {
    }
}
