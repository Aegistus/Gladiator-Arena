using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Equipment : NetworkBehaviour
{
    public enum Usage
    {
        Primary, Secondary, Both, Either
    }

    public Usage usage;
}
