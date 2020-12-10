using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public enum Usage
    {
        Primary, Secondary, Both, Either
    }

    public Usage usage;
    
    public bool Equipped { get; private set; }

    public void Equip(Transform hand)
    {
        transform.position = hand.position;
        transform.rotation = hand.rotation;
        transform.parent = hand;
        gameObject.SetActive(true);
    }

    public void UnEquip()
    {
        gameObject.SetActive(false);
    }
}
