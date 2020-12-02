using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    public LayerMask groundLayer;
    public Transform lookDirection;

    public MovementState CurrentState => (MovementState)stateMachine.CurrentState;
    public Vector3 Velocity => rb.velocity;

    private float velocityMod = 1f;
    private StateMachine stateMachine;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        stateMachine = new StateMachine();
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(Idling), new Idling(gameObject) },
            {typeof(Walking), new Walking(gameObject) },
            {typeof(Jumping), new Jumping(gameObject) },
            {typeof(Falling), new Falling(gameObject) },
            {typeof(Sprinting), new Sprinting(gameObject) },
        };
        stateMachine.SetStates(states, typeof(Idling));
    }

    public void SetHorizontalVelocity(Vector3 velocity)
    {
        rb.velocity = new Vector3(velocity.x * velocityMod, rb.velocity.y, velocity.z * velocityMod);
    }

    public void SetVerticalVelocity(float vertVelocity)
    {
        rb.velocity = new Vector3(rb.velocity.x, vertVelocity, rb.velocity.z);
    }

    public void AddVelocity(Vector3 additional)
    {
        rb.velocity += additional * velocityMod;
    }

    public void ModifyVelocity(float percentChange)
    {
        velocityMod = percentChange;
        rb.velocity *= velocityMod;
    }

    private void Update()
    {
        stateMachine.ExecuteState();
        //transform.Translate(velocity * Time.deltaTime, Space.World);
    }

}
