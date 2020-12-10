using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentAnimation : MonoBehaviour
{
    private Animator anim;
    private AgentMovement movement;
    private AgentCombat combat;
    private AgentEquipment equipment;

    private int runningHash = Animator.StringToHash("Running");
    private int idleHash = Animator.StringToHash("Idling");
    private int walkingHash = Animator.StringToHash("Walking");
    private int jumpingHash = Animator.StringToHash("Jumping");
    private int fallingHash = Animator.StringToHash("Falling");
    private int crouchingHash = Animator.StringToHash("Crouching");
    private int climbingHash = Animator.StringToHash("Climbing");
    private int vaultingHash = Animator.StringToHash("Vaulting");
    private int slidingHash = Animator.StringToHash("Sliding");
    private int leftSlashHash = Animator.StringToHash("LeftSlashing");
    private int rightSlashHash = Animator.StringToHash("RightSlashing");
    private int stabbing = Animator.StringToHash("Stabbing");

    private Dictionary<Type, int> stateToHash;
    private Dictionary<AgentEquipment.EquipmentStance, int> stanceLayers;

    private int prevStateHash = 0;
    private Type nextStateType;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        movement = GetComponent<AgentMovement>();
        combat = GetComponent<AgentCombat>();
        equipment = GetComponent<AgentEquipment>();
        stateToHash = new Dictionary<Type, int>()
        {
            {typeof(Sprinting), runningHash },
            {typeof(Idling), idleHash },
            {typeof(Walking), walkingHash },
            {typeof(Jumping), jumpingHash },
            {typeof(Falling), fallingHash },
            {typeof(Crouching), crouchingHash },
            {typeof(Climbing), climbingHash},
            {typeof(Vaulting), vaultingHash},
            {typeof(Sliding), slidingHash },
            {typeof(ReleaseState), rightSlashHash },
        };
        stanceLayers = new Dictionary<AgentEquipment.EquipmentStance, int>()
        {
            {AgentEquipment.EquipmentStance.OneHandedShield, anim.GetLayerIndex("One Handed Shield") },
            {AgentEquipment.EquipmentStance.TwoHanded, anim.GetLayerIndex("Two Handed") },
        };
    }

    private void Start()
    {
        movement.StateMachine.OnStateChange += ChangeAnimationState;
        combat.StateMachine.OnStateChange += ChangeAnimationState;
        equipment.OnStanceChange += ChangeAnimationLayer;
    }

    private void ChangeAnimationLayer(AgentEquipment.EquipmentStance stance)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }
        anim.SetLayerWeight(stanceLayers[stance], 1);
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
