using UnityEngine;

public class Jumping : MovementState
{
    private float jumpForce = 4.85f;
    private float airMoveSpeed = 2f;
    Vector3 startingVelocity;

    public Jumping(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Falling), Falling));
        transitionsTo.Add(new Transition(typeof(Idling), OnGround, Not(Rising), Not(Falling)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Jumping");
        movement.SetVerticalVelocity(jumpForce);
        startingVelocity = movement.Velocity * .75f;
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
        if (newVelocity.sqrMagnitude > 0)
        {
            movement.SetHorizontalVelocity(startingVelocity + newVelocity * airMoveSpeed);
        }
    }
}
