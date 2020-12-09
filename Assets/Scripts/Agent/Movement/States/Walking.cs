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
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
    }

    public override void AfterExecution()
    {
        if (movement.velocity.y > 0)
        {
            movement.AddVerticalVelocity(0);
        }
    }

    public override void BeforeExecution()
    {
        movement.SetHorizontalVelocity(Vector3.zero);
    }

    Vector3 inputVelocity;
    public override void DuringExecution()
    {
        inputVelocity = GetAgentMovementInput();
        movement.SetHorizontalVelocity(inputVelocity * moveSpeed);
        RotateAgentModelToDirection(inputVelocity);
        KeepGrounded();
    }

}
