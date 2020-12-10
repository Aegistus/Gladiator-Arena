using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseState : CombatState
{
    public ReleaseState(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(ReadyState), TimerComplete));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Releasing");
        timerMax = 1f;
        timer = 0;
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
    }
}
