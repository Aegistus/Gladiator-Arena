using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentEquipment : MonoBehaviour
{
    public Transform primaryHand;
    public Transform secondaryHand;
    public int maxCarriedEquipment = 4;

    public Equipment PrimaryEquipped { get; private set; }
    public Equipment SecondaryEquipped { get; private set; }

    public List<Equipment> primaryEquipment;
    public List<Equipment> secondaryEquipment;

    private AgentAnimation agentAnim;
    private int primaryIndex;
    private int secondaryIndex;

    private void Awake()
    {
        agentAnim = GetComponentInChildren<AgentAnimation>();
    }

    public void GoToNextPrimaryEquipment()
    {
        if (primaryEquipment.Count > 0)
        {
            primaryIndex++;
            if (primaryIndex >= primaryEquipment.Count)
            {
                primaryIndex = 0;
            }
            PrimaryEquipped?.UnEquip();
            PrimaryEquipped = primaryEquipment[primaryIndex];
            PrimaryEquipped?.Equip(primaryHand);
            if (PrimaryEquipped.usage == Equipment.Usage.Both)
            {
                SecondaryEquipped?.UnEquip();
                SecondaryEquipped = null;
            }
        }
        else if (primaryEquipment.Count == 0)
        {
            PrimaryEquipped = null;
        }
        agentAnim.ChangeAnimationType(PrimaryEquipped.animationLayer);
    }

    public void GoToNextSecondaryEquipment()
    {
        if (PrimaryEquipped.usage != Equipment.Usage.Both)
        {
            if (secondaryEquipment.Count > 0)
            {
                secondaryIndex++;
                if (secondaryIndex >= secondaryEquipment.Count)
                {
                    secondaryIndex = 0;
                }
                SecondaryEquipped?.UnEquip();
                SecondaryEquipped = secondaryEquipment[secondaryIndex];
                SecondaryEquipped?.Equip(secondaryHand);
            }
            else if (secondaryEquipment.Count == 0)
            {
                SecondaryEquipped = null;
            }
        }
    }

    public void PickupEquipment(Equipment newEquipment)
    {
        if (primaryEquipment.Count + secondaryEquipment.Count < maxCarriedEquipment)
        {
            if (newEquipment.usage == Equipment.Usage.Primary || newEquipment.usage == Equipment.Usage.Both)
            {
                primaryEquipment.Add(newEquipment);
            }
            else
            {
                secondaryEquipment.Add(newEquipment);
            }
        }
    }

}
