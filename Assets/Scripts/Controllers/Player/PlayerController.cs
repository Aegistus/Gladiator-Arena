using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    public GameObject playerCam;

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Forwards = Input.GetKey(KeyCode.W);
        Backwards = Input.GetKey(KeyCode.S);
        Left = Input.GetKey(KeyCode.A);
        Right = Input.GetKey(KeyCode.D);
        Run = Input.GetKey(KeyCode.LeftShift);
        Jump = Input.GetKey(KeyCode.Space);
        Crouch = Input.GetKey(KeyCode.LeftControl);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        playerCam.SetActive(true);
    }
}
