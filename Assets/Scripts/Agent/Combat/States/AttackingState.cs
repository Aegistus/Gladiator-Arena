using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : CombatState
{
    public AttackingState(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(ReadyState), TimerComplete));
    }

    public override void AfterExecution()
    {
        charController.enabled = true;
    }

    public override void BeforeExecution()
    {
        Debug.Log("Releasing");
        timer = 0;
        charController.enabled = false;
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
    }
}
