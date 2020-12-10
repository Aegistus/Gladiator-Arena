using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : AgentController
{
    public GameObject playerCam;

    private AgentEquipment equipment;

    private void Start()
    {
        equipment = GetComponent<AgentEquipment>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Attack = Input.GetMouseButton(0);
        Block = Input.GetMouseButton(1);
        Forwards = Input.GetKey(KeyCode.W);
        Backwards = Input.GetKey(KeyCode.S);
        Left = Input.GetKey(KeyCode.A);
        Right = Input.GetKey(KeyCode.D);
        Run = Input.GetKey(KeyCode.LeftShift);
        Jump = Input.GetKey(KeyCode.Space);
        Crouch = Input.GetKey(KeyCode.LeftControl);
        if (Input.mouseScrollDelta.y > 0)
        {
            equipment.GoToNextSecondaryEquipment();
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            equipment.GoToNextPrimaryEquipment();
        }
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        playerCam.SetActive(true);
    }
}
