using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : StandingState
{
    public Running(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Walking), Not(AtRunningSpeed), AtWalkingSpeed));
        transitionsTo.Add(new Transition(typeof(Idling), Not(AtWalkingSpeed)));
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
