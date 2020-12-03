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
        if (Physics.BoxCast(transform.position, Vector3.one / 3, Vector3.down, transform.rotation, .5f, groundLayer))
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

}
