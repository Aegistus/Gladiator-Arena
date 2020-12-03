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

    Quaternion targetRotation, currentRotation;
    protected void RotateAgentModelToDirection(Vector3 newVelocity)
    {
        // make the agent's model rotate towards the direction of movement
        currentRotation = movement.agentModel.rotation;
        movement.agentModel.LookAt(newVelocity + movement.agentModel.position);
        targetRotation = movement.agentModel.rotation;
        movement.agentModel.rotation = currentRotation;
        movement.agentModel.rotation = Quaternion.Lerp(currentRotation, targetRotation, 50f  * Time.deltaTime);
    }
}
