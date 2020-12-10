using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CombatState : State
{
    protected AgentController agentController;
    protected CharacterController charController;

    public Func<bool> AttackInput => () => agentController.Attack;
    public Func<bool> RightSlashInput => () => agentController.AttackDirection == AttackDirection.Right;
    public Func<bool> LeftSlashInput => () => agentController.AttackDirection == AttackDirection.Left;
    public Func<bool> StabInput => () => agentController.AttackDirection == AttackDirection.Stab;

    protected CombatState(GameObject gameObject) : base(gameObject)
    {
        agentController = gameObject.GetComponent<AgentController>();
        charController = gameObject.GetComponent<CharacterController>();
    }
}
