using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : StandingState
{
    public Running(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Walking), Not(StandingStill), AboveWalkingSpeed));
        transitionsTo.Add(new Transition(typeof(Idling), Not(AboveWalkingSpeed)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Running");
    }

    public override void DuringExecution()
    {

    }
}
