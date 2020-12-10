using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyState : CombatState
{


    public ReadyState(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(ReleaseState), AttackInput));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Ready");
    }

    public override void DuringExecution()
    {

    }
}
