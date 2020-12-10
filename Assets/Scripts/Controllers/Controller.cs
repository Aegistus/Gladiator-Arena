using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class Controller : NetworkBehaviour
{
    public bool Attack { get; protected set; }
    public bool Block { get; protected set; }
    public bool Forwards { get; protected set; }
    public bool Backwards { get; protected set; }
    public bool Left { get; protected set; }
    public bool Right { get; protected set; }
    public bool Jump { get; protected set; }
    public bool Crouch { get; protected set; }
    public bool Run { get; protected set; }
}
