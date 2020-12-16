using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    public Equipment CurrentlyEquipped { get; private set; }

    public void Equip(Equipment equipment)
    {
        CurrentlyEquipped = equipment;
        CurrentlyEquipped.transform.position = transform.position;
        CurrentlyEquipped.transform.rotation = transform.rotation;
        CurrentlyEquipped.transform.parent = transform;
    }

    public Equipment UnEquip()
    {
        Equipment toReturn = CurrentlyEquipped;
        CurrentlyEquipped = null;
        return toReturn;
    }
}
