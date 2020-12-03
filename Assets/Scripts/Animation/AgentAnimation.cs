using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentAnimation : MonoBehaviour
{
    private Animator anim;
    private MovementController movement;

    private int runningHash = Animator.StringToHash("Running");
    private int idleHash = Animator.StringToHash("Idling");
    private int walkingHash = Animator.StringToHash("Walking");
    private int jumpingHash = Animator.StringToHash("Jumping");
    private int fallingHash = Animator.StringToHash("Falling");
    private int crouchingHash = Animator.StringToHash("Crouching");

    private Dictionary<Type, int> stateToHash;

    private int prevStateHash = 0;
    private Type nextStateType;


    private void Awake()
    {
        stateToHash = new Dictionary<Type, int>()
        {
            {typeof(Sprinting), runningHash },
            {typeof(Idling), idleHash },
            {typeof(Walking), walkingHash },
            {typeof(Jumping), jumpingHash },
            {typeof(Falling), fallingHash },
            {typeof(Crouching), crouchingHash },
        };
        anim = GetComponentInChildren<Animator>();
        movement = GetComponent<MovementController>();
    }

    private void Start()
    {
        movement.StateMachine.OnStateChange += ChangeAnimationState;
    }

    public void ChangeAnimationState(State newState)
    {
        nextStateType = newState.GetType();
        if (prevStateHash != 0)
        {
            anim.SetBool(prevStateHash, false);
        }
        if (stateToHash.ContainsKey(nextStateType))
        {
            anim.SetBool(stateToHash[nextStateType], true);
            prevStateHash = stateToHash[nextStateType];
        }
    }
}
