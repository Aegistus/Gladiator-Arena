using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public AgentMovement movement;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            movement.AddVelocity(transform.forward * 10 * Time.deltaTime);
        }
    }
}
