using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : OnGroundState
{
    private float moveSpeed = 7.5f;

    public Walking(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Run");
        transitionsTo.Add(new Transition(typeof(Idling), Not(MoveKeys)));
        transitionsTo.Add(new Transition(typeof(Sprinting), Shift));
        transitionsTo.Add(new Transition(typeof(Jumping), Spacebar, OnGround));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        movement.SetHorizontalVelocity(Vector3.zero);
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        if (movement.Velocity.magnitude < moveSpeed)
        {
            newVelocity = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                newVelocity += movement.lookDirection.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                newVelocity += -movement.lookDirection.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                newVelocity += -movement.lookDirection.right;
            }
            if (Input.GetKey(KeyCode.D))
            {
                newVelocity += movement.lookDirection.right;
            }
            newVelocity = newVelocity.normalized;
            movement.SetHorizontalVelocity(newVelocity * moveSpeed);
        }
        KeepGrounded();
    }

}
