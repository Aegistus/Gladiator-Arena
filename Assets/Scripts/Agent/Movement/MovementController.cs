using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MovementController : MonoBehaviour
{
    public LayerMask groundLayer;
    public Transform lookDirection;
    public Transform agentModel;
    public WallDetector wallDetectorUpper;
    public WallDetector wallDetectorLower;
    public LedgeDetector ledgeDetector;
    public WallDetector vaultOtherSideDetector;

    public MovementState CurrentState => (MovementState)StateMachine.CurrentState;
    public Vector3 velocity;

    public StateMachine StateMachine { get; private set; }
    private CharacterController charController;
    private bool inAir = false;

    private void Awake()
    {
        StateMachine = new StateMachine();
        charController = GetComponent<CharacterController>();
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(Idling), new Idling(gameObject) },
            {typeof(Walking), new Walking(gameObject) },
            {typeof(Jumping), new Jumping(gameObject) },
            {typeof(Falling), new Falling(gameObject) },
            {typeof(Sprinting), new Sprinting(gameObject) },
            {typeof(Crouching), new Crouching(gameObject) },
            {typeof(Climbing), new Climbing(gameObject) },
            {typeof(Vaulting), new Vaulting(gameObject) },
            {typeof(Sliding), new Sliding(gameObject) },
        };
        StateMachine.SetStates(states, typeof(Idling));
    }

    public void SetHorizontalVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void SetVerticalVelocity(float vertVelocity)
    {
        velocity.y = vertVelocity;
    }

    public void SetInAir(bool inAir)
    {
        this.inAir = inAir;
    }

    private void Update()
    {
        StateMachine.ExecuteState();
        if (inAir)
        {
            velocity.y += -9.8f * Time.deltaTime;
        }
        else
        {
            velocity.y = 0;
        }
        charController.Move(velocity * Time.deltaTime);
    }
}
