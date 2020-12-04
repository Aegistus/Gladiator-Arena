using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementState : State
{
    protected LayerMask groundLayer;
    protected MovementController movement;
    protected Controller controller;
    protected Rigidbody rb;
    protected List<string> animationNames = new List<string>();
    protected List<string> soundNames = new List<string>();

    public Func<bool> Move => () => controller.Forwards || controller.Backwards || controller.Right || controller.Left;
    public Func<bool> Jump => () => controller.Jump;
    public Func<bool> PrimaryAction => () => Input.GetMouseButtonDown(0);
    //public Func<bool> RightClick => () => Input.GetMouseButton(1);
    public Func<bool> Run => () => controller.Run;
    public Func<bool> Crouch => () => controller.Crouch;
    public Func<bool> OnGround => () => IsGrounded();
    public Func<bool> NextToWall => () => IsNextToWall();
    public Func<bool> LedgeInReach => () => movement.ledgeDetector.CollidingWith == 0;
    public Func<bool> FacingHighWall => () => movement.wallDetectorUpper.CollidingWith > 0;
    public Func<bool> FacingLowWall => () => movement.wallDetectorLower.CollidingWith > 0;
    public Func<bool> OtherSideOfVaultOpen => () => movement.vaultOtherSideDetector.CollidingWith == 0;
    public Func<bool> Rising => () => rb.velocity.y > 0;
    public Func<bool> Falling => () => rb.velocity.y < -.1f;

    public MovementState(GameObject gameObject) : base(gameObject)
    {
        movement = gameObject.GetComponent<MovementController>();
        controller = gameObject.GetComponent<Controller>();
        groundLayer = gameObject.GetComponent<MovementController>().groundLayer;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private bool IsGrounded()
    {
        if (Physics.BoxCast(transform.position, Vector3.one / 5, Vector3.down, transform.rotation, .5f, groundLayer))
        {
            return true;
        }
        return false;
    }

    private bool IsNextToWall()
    {
        if (Physics.Raycast(new Ray(transform.position, transform.right), 1f, groundLayer))
        {
            return true;
        }
        if (Physics.Raycast(new Ray(transform.position, -transform.right), 1f, groundLayer))
        {
            return true;
        }
        return false;
    }

    Quaternion targetRotation, currentRotation;
    protected void RotateAgentModelToDirection(Vector3 newVelocity)
    {
        // make the agent's model rotate towards the direction of movement
        currentRotation = movement.agentModel.rotation;
        movement.agentModel.LookAt(newVelocity + movement.agentModel.position);
        targetRotation = movement.agentModel.rotation;
        movement.agentModel.rotation = currentRotation;
        movement.agentModel.rotation = Quaternion.Lerp(currentRotation, targetRotation, 10f * Time.deltaTime);
    }

    Vector3 newVelocity;
    protected Vector3 GetAgentMovementInput()
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
        RotateAgentModelToDirection(newVelocity);
        newVelocity = newVelocity.normalized;
        return newVelocity;
    }

}
