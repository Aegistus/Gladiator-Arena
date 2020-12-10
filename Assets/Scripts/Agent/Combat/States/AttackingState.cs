using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackingState : CombatState
{
    protected AgentAnimEvents animEvents;

    private bool animationFinished = false;

    public Func<bool> AnimationFinished => () => animationFinished;

    public AttackingState(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(ReadyState), AnimationFinished));
        animEvents = gameObject.GetComponentInChildren<AgentAnimEvents>();
        animEvents.OnAnimationEvent += CheckAnimationEvent;
    }

    private void CheckAnimationEvent(EventType eventType)
    {
        if (eventType == EventType.Finish)
        {
            animationFinished = true;
        }
    }

    public override void AfterExecution()
    {
        charController.enabled = true;
    }

    public override void BeforeExecution()
    {
        Debug.Log("Releasing");
        charController.enabled = false;
        animationFinished = false;
    }

    public override void DuringExecution()
    {

    }
}
