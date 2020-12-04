using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnGroundState : MovementState
{
    RaycastHit rayHit;

    protected OnGroundState(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Crouching), Crouch, Not(Move)));
        transitionsTo.Add(new Transition(typeof(Climbing), Jump, FacingWall, LedgeInReach));
        transitionsTo.Add(new Transition(typeof(Jumping), Jump, OnGround, Not(FacingWall)));
    }

    protected void KeepGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out rayHit, .75f, groundLayer))
        {
            transform.position = rayHit.point + Vector3.up * .5f;
        }
    }
}
