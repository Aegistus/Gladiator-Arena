﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyState : CombatState
{

    public ReadyState(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Blocking), BlockInput));
        transitionsTo.Add(new Transition(typeof(RightSlashing), AttackInput, RightSlashInput));
        transitionsTo.Add(new Transition(typeof(LeftSlashing), AttackInput, LeftSlashInput));
        transitionsTo.Add(new Transition(typeof(Stabbing), AttackInput, StabInput));
        transitionsTo.Add(new Transition(typeof(EquippingPrimary), SwitchPrimaryInput));
        transitionsTo.Add(new Transition(typeof(EquippingSecondary), SwitchSecondaryInput));
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
