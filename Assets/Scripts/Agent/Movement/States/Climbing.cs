using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Climbing : MovementState
{
    Func<bool> TimerUp => () => timer >= timerMax;

    float timerMax = 3f;
    float timer;

    public Climbing(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), TimerUp));
    }

    public override void AfterExecution()
    {
        transform.position += movement.agentModel.forward + (Vector3.up * 1.75f);
    }

    RaycastHit rayHit;
    public override void BeforeExecution()
    {
        Debug.Log("Climbing");

        //if (Physics.Raycast(transform.position, movement.agentModel.forward, out rayHit, 10f, groundLayer))
        //{
        //    transform.position = rayHit.point;
        //    transform.position += movement.agentModel.forward * .1f;
        //    Debug.Log("Moving transform to wall");
        //}
        timer = 0;
    }

    public override void DuringExecution()
    {
        movement.SetHorizontalVelocity(movement.agentModel.forward * 3f);
        timer += Time.deltaTime;
    }
}
