using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : StandingState
{
    public Walking(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), Not(AboveWalkingSpeed)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Walking");
    }

    public override void DuringExecution()
    {
        
    }
}
