using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Vaulting : MovementState
{
    Func<bool> TimerUp => () => timer >= timerMax;

    float timerMax = 1.1f;
    float timer;

    float vaultSpeed = 3f;
    float vaultHeight = .75f;

    public Vaulting(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), TimerUp));
    }

    public override void AfterExecution()
    {
        if (TimerUp())
        {
            transform.position += movement.agentModel.forward;
        }
        rb.isKinematic = false;
        rb.detectCollisions = true;
    }

    public override void BeforeExecution()
    {
        timer = 0;
        rb.isKinematic = true;
        rb.detectCollisions = false;
        //transform.position += vaultHeight * Vector3.up;
    }
    
    public override void DuringExecution()
    {
        transform.Translate(movement.agentModel.forward * vaultSpeed * Time.deltaTime);
        timer += Time.deltaTime;
    }
}
