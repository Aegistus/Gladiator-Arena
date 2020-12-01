using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idling : StandingState
{

    public Idling(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Walking), AboveWalkingSpeed, Not(StandingStill)));
    }

    public override void AfterExecution()
    {
        
    }

    public override void BeforeExecution()
    {
        Debug.Log("Idling");
    }

    public override void DuringExecution()
    {

    }
}
