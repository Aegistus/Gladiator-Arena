using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StandingState : MovementState
{
    protected StandingState(GameObject gameObject) : base(gameObject)
    {
    }
}
