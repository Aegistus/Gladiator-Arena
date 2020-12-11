﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippingSecondary : CombatState
{
    private bool animationDone = false;
    private AgentAnimEvents animEvents;
    private AgentEquipment equipment;

    public EquippingSecondary(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Equipping");
        transitionsTo.Add(new Transition(typeof(ReadyState), () => animationDone));
        equipment = gameObject.GetComponent<AgentEquipment>();
        animEvents = gameObject.GetComponentInChildren<AgentAnimEvents>();
    }

    private void CheckForWhenToAddWeapon(EventType eventType)
    {
        if (eventType == EventType.Finish)
        {
            equipment.GoToNextSecondaryEquipment();
            animationDone = true;
        }
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
        animEvents.OnAnimationEvent -= CheckForWhenToAddWeapon;
    }

    public override void BeforeExecution()
    {
        anim.SetBool(animationHash, true);
        animationDone = false;
        animEvents.OnAnimationEvent += CheckForWhenToAddWeapon;
    }

    public override void DuringExecution()
    {

    }
}