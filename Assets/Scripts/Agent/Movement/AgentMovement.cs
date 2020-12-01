using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentMovement : MonoBehaviour
{
    public Vector3 CurrentVelocity { get { return rb.velocity; } }

    public float walkSpeed = 1f;
    public float runSpeed = 5f;

    private StateMachine movementStateMachine;
    private Rigidbody rb;

    void Awake()
    {
        movementStateMachine = new StateMachine();
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(Idling), new Idling(gameObject) },
            {typeof(Walking), new Walking(gameObject) },
            {typeof(Running), new Running(gameObject) },
        };
        movementStateMachine.SetStates(states, typeof(Idling));
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movementStateMachine.ExecuteState();
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    public void AddVelocity(Vector3 velocity)
    {
        rb.velocity += velocity;
    }

}
