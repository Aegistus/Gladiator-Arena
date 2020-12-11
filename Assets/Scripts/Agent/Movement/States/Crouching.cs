using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouching : MovementState
{
    public Crouching(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Crouching");
        transitionsTo.Add(new Transition(typeof(Idling), Not(Crouch)));
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Crouching");
        anim.SetBool(animationHash, true);
        movement.SetHorizontalVelocity(Vector3.zero);
    }

    public override void DuringExecution()
    {

    }
}
