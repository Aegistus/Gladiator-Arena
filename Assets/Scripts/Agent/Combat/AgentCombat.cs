using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentCombat : MonoBehaviour
{
    private StateMachine combatStateMachine;

    private void Awake()
    {
        combatStateMachine = new StateMachine();
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(ReadyState), new ReadyState(gameObject) },
            {typeof(ReleaseState), new ReleaseState(gameObject) },
        };
        combatStateMachine.SetStates(states, typeof(ReadyState));
    }

    private void Update()
    {
        combatStateMachine.ExecuteState();
    }
}
