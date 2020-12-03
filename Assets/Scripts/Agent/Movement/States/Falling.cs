using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MovementState
{
    private float airMoveSpeed = 1f;
    Vector3 startingVelocity;

    public Falling(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Fall");
        transitionsTo.Add(new Transition(typeof(Idling), Not(Falling), OnGround));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Falling");
        //anim.Play(animationNames[0]);
        startingVelocity = movement.Velocity;
    }

    Vector3 newVelocity;
    public override void DuringExecution()
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
        if (newVelocity.sqrMagnitude> 0)
        {
            movement.SetHorizontalVelocity(startingVelocity + newVelocity * airMoveSpeed);
        }
    }
}
