using UnityEngine;

public class Jumping : MovementState
{
    private float jumpForce = 5f;
    private float airMoveSpeed = 2f;
    Vector3 startingVelocity;

    public Jumping(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Falling), Falling));
        transitionsTo.Add(new Transition(typeof(Idling), OnGround, Not(Rising), Not(Falling)));
    }

    public override void AfterExecution()
    {
        movement.SetInAir(false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Jumping");
        startingVelocity = movement.velocity * .75f;
        movement.SetInAir(true);
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        newVelocity = GetAgentMovementInput();
        movement.SetHorizontalVelocity(startingVelocity + newVelocity * airMoveSpeed);
        movement.SetVerticalVelocity(jumpForce);
        RotateAgentModelToDirection(newVelocity);
    }
}
