using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Vaulting : MovementState
{
    Func<bool> TimerUp => () => timer >= timerMax;

    float timerMax = 1f;
    float timer;

    float vaultSpeed = .02f;

    public Vaulting(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), TimerUp));
    }

    public override void AfterExecution()
    {
        if (TimerUp())
        {
            transform.position += movement.agentModel.forward * .25f;
        }
        charController.detectCollisions = true;
    }

    public override void BeforeExecution()
    {
        timer = 0;
        charController.detectCollisions = false;
    }
    
    public override void DuringExecution()
    {
        transform.Translate(movement.agentModel.forward * vaultSpeed * Time.deltaTime);
        timer += Time.deltaTime;
    }
}
