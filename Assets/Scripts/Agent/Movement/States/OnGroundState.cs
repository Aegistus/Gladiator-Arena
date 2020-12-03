using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnGroundState : MovementState
{
    RaycastHit rayHit;

    protected OnGroundState(GameObject gameObject) : base(gameObject)
    {

    }

    protected void KeepGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out rayHit, .75f, groundLayer))
        {
            transform.position = rayHit.point + Vector3.up * .5f;
        }
    }

    protected void RotateToFaceCameraDirection()
    {
        movement.agentModel.rotation = movement.lookDirection.rotation;
    }
}
