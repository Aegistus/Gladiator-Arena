using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentEquipment : MonoBehaviour
{
    public Transform primaryHand;
    public Transform secondaryHand;
    public int maxCarriedEquipment = 4;

    public event Action<EquipmentStance> OnStanceChange;

    public Equipment PrimaryEquipped { get; private set; }
    public Equipment SecondaryEquipped { get; private set; }
    public EquipmentStance CurrentStance { get; private set; }

    public List<Equipment> primaryEquipment;
    public List<Equipment> secondaryEquipment;

    private int primaryIndex;
    private int secondaryIndex;

    private Animator anim;
    private Dictionary<EquipmentStance, int> equipmentStanceLayers = new Dictionary<EquipmentStance, int>();

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        equipmentStanceLayers.Add(EquipmentStance.TwoHanded, anim.GetLayerIndex("Two Handed"));
        equipmentStanceLayers.Add(EquipmentStance.OneHandedShield, anim.GetLayerIndex("One Handed Shield"));
    }

    public enum EquipmentStance
    {
        TwoHanded, OneHandedShield
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
        UpdateCurrentEquipmentStance();
    }

    public void GoToNextSecondaryEquipment()
    {
        if (PrimaryEquipped?.usage != Equipment.Usage.Both)
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
        UpdateCurrentEquipmentStance();
    }

    public void UpdateCurrentEquipmentStance()
    {
        if (PrimaryEquipped.usage == Equipment.Usage.Both)
        {
            CurrentStance = EquipmentStance.TwoHanded;
        }
        else
        {
            CurrentStance = EquipmentStance.OneHandedShield;
        }
        // set all stance layers to 0 weight
        foreach (var stance in equipmentStanceLayers)
        {
            anim.SetLayerWeight(stance.Value, 0);
        }
        anim.SetLayerWeight(equipmentStanceLayers[CurrentStance], 1);
        OnStanceChange?.Invoke(CurrentStance);
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
