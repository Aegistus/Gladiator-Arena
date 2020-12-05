using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentEquipment : MonoBehaviour
{
    public Transform primaryHand;
    public Transform secondaryHand;
    public int maxCarriedEquipment = 4;

    public Equipment primaryEquipped { get; private set; }
    public Equipment secondaryEquipped { get; private set; }

    private List<Equipment> carriedEquipment;
    private int primaryIndex;
    private int secondaryIndex;

    public void GoToNextPrimaryEquipment()
    {
        bool compatibleEquipment = false;
        int index = primaryIndex;
        index++;
        while (compatibleEquipment == false && index != primaryIndex)
        {
            if (index >= carriedEquipment.Count)
            {
                index = 0;
            }
            if (carriedEquipment[index].usage != Equipment.Usage.Primary
                && carriedEquipment[index].usage != Equipment.Usage.TwoHanded)
            {
                compatibleEquipment = true;
                primaryEquipped.UnEquip();
                primaryEquipped = carriedEquipment[index];
                primaryEquipped.Equip(primaryHand);
            }
            index++;
        }
    }

    public void GoToNextSecondaryEquipment()
    {
        bool compatibleEquipment = false;
        int index = secondaryIndex;
        index++;
        while (compatibleEquipment == false && index != secondaryIndex)
        {
            if (index >= carriedEquipment.Count)
            {
                index = 0;
            }
            if (carriedEquipment[index].usage != Equipment.Usage.Secondary)
            {
                compatibleEquipment = true;
                secondaryEquipped.UnEquip();
                secondaryEquipped = carriedEquipment[index];
                secondaryEquipped.Equip(secondaryHand);
            }
            index++;
        }
    }

    public void PickupEquipment(Equipment newEquipment)
    {
        if (carriedEquipment.Count < maxCarriedEquipment)
        {
            carriedEquipment.Add(newEquipment);
        }
    }

}
