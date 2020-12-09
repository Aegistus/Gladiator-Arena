using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MovementState
{
    private float airMoveSpeed = .01f;
    Vector3 startingVelocity;

    public Falling(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Fall");
        transitionsTo.Add(new Transition(typeof(Idling), Not(Falling), OnGround));
    }

    public override void AfterExecution()
    {
        movement.SetInAir(false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Falling");
        //anim.Play(animationNames[0]);
        startingVelocity = movement.velocity;
        movement.SetInAir(true);
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        newVelocity = GetAgentMovementInput();
        if (newVelocity.sqrMagnitude > 0)
        {
            movement.SetHorizontalVelocity(startingVelocity + newVelocity * airMoveSpeed);
        }
        RotateAgentModelToDirection(newVelocity);
    }
}
