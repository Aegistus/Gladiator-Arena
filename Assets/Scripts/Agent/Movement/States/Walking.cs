using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : OnGroundState
{
    private float moveSpeed = 3f;

    public Walking(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Run");
        transitionsTo.Add(new Transition(typeof(Idling), Not(Move)));
        transitionsTo.Add(new Transition(typeof(Sprinting), Run));
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
        movement.SetHorizontalVelocity(Vector3.zero);
        anim.Play("Walk");
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
        }
        KeepGrounded();
        RotateToFaceCameraDirection();
    }

}
