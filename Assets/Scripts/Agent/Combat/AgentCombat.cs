using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentCombat : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }

    private void Awake()
    {
        StateMachine = new StateMachine();
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(ReadyState), new ReadyState(gameObject) },
            {typeof(Stabbing), new Stabbing(gameObject) },
            {typeof(RightSlashing), new RightSlashing(gameObject) },
            {typeof(LeftSlashing), new LeftSlashing(gameObject) },
            {typeof(EquippingPrimary), new EquippingPrimary(gameObject) },
            {typeof(EquippingSecondary), new EquippingSecondary(gameObject) },
        };
        StateMachine.SetStates(states, typeof(ReadyState));
    }

    private void Update()
    {
        StateMachine.ExecuteState();
    }
}
