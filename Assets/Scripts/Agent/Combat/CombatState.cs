using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CombatState : State
{
    protected Controller agentController;

    protected float timer;
    protected float timerMax;

    public Func<bool> TimerComplete => () => timer >= timerMax;
    public Func<bool> AttackInput => () => agentController.Attack;

    protected CombatState(GameObject gameObject) : base(gameObject)
    {
        agentController = gameObject.GetComponent<Controller>();
    }
}
