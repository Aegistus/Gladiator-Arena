using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sliding : MovementState
{
    Func<bool> TimerUp => () => timer >= timerMax;

    float timerMax = 1.1f;
    float timer;

    float slideSpeed = 12f;

    public Sliding(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Sprinting), TimerUp));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        movement.SetHorizontalVelocity(movement.agentModel.forward * slideSpeed);
        timer = 0;
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
    }
}
