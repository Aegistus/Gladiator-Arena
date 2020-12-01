using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    public float agentSpeed = 1f;

    private StateMachine movementStateMachine;

    void Awake()
    {
        movementStateMachine = new StateMachine();
    }

    void Update()
    {
        movementStateMachine.ExecuteState();
    }
}
