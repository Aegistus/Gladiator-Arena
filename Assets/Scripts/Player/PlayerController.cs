using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform lookDirection;

    private AgentMovement movement;

    private void Awake()
    {
        movement = GetComponent<AgentMovement>();
    }

    Vector3 newVelocity;
    private void Update()
    {
        newVelocity = Vector3.zero;
        newVelocity += lookDirection.forward * movement.walkSpeed * Input.GetAxis("Vertical");
        newVelocity += lookDirection.right * movement.walkSpeed * Input.GetAxis("Horizontal");
        movement.SetVelocity(newVelocity);
    }
}
