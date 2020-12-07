using UnityEngine;
using Mirror;

public class NetworkPhysics : NetworkBehaviour
{
    public Rigidbody rb;

    [SyncVar]//all the essental varibles of a rigidbody
    public Vector3 Velocity;
    [SyncVar]
    public Quaternion Rotation;
    [SyncVar]
    public Vector3 Position;
    [SyncVar]
    public Vector3 AngularVelocity;

    void Update()
    {
        if (netIdentity.isServer)//if we are the server update the varibles with our cubes rigidbody info
        {
            Position = rb.position;
            Rotation = rb.rotation;
            Velocity = rb.velocity;
            AngularVelocity = rb.angularVelocity;
            rb.position = Position;
            rb.rotation = Rotation;
            rb.velocity = Velocity;
            rb.angularVelocity = AngularVelocity;
        }
        if (netIdentity.isClient)//if we are a client update our rigidbody with the servers rigidbody info
        {
            rb.position = Position + Velocity * (float)NetworkTime.rtt;//account for the lag and update our varibles
            rb.rotation = Rotation * Quaternion.Euler(AngularVelocity * (float)NetworkTime.rtt);
            rb.velocity = Velocity;
            rb.angularVelocity = AngularVelocity;
        }
    }

    [Command]//function that runs on server when called by a client
    public void CmdResetPose()
    {
        rb.position = Vector3.up;
        rb.velocity = Vector3.zero;
    }

    public void SetVelocity(Vector3 velocity)//apply force on the client-side to reduce the appearance of lag and then apply it on the server-side
    {
        if (!isLocalPlayer)
        {
            return;
        }
        rb.velocity = velocity;
        CmdSetVelocity(velocity);
    }

    [Command]//function that runs on server when called by a client
    public void CmdSetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;//apply the force on the server side
    }

}