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
    public Vector3 Velocity => charController.velocity;

    private float velocityMod = 1f;
    public StateMachine StateMachine { get; private set; }
    private CharacterController charController;

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
        charController.Move(new Vector3(velocity.x * velocityMod, charController.velocity.y, velocity.z * velocityMod));
    }

    public void SetVerticalVelocity(float vertVelocity)
    {
        charController.Move(new Vector3(charController.velocity.x, vertVelocity, charController.velocity.z));
    }

    private void Update()
    {
        StateMachine.ExecuteState();
        //transform.Translate(velocity * Time.deltaTime, Space.World);
    }
}
