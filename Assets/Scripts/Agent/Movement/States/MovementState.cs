using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementState : State
{
    protected AgentMovement movement;

    protected MovementState(GameObject gameObject) : base(gameObject)
    {
        movement = gameObject.GetComponent<AgentMovement>();
    }
}
