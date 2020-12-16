using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AnimationStance
{
    TwoHanded, OneHandedShield
}

public class AgentEquipment : MonoBehaviour
{
    public EquipmentSlot primarySlot;
    public EquipmentSlot secondarySlot;
    public List<Equipment> availablePrimaryEquipment;
    public List<Equipment> availableSecondaryEquipment;

    public AnimationStance CurrentStance { get; private set; }

    private int primaryIndex;
    private int secondaryIndex;
    private Animator anim;
    private Dictionary<AnimationStance, int> equipmentStanceLayers = new Dictionary<AnimationStance, int>();

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        equipmentStanceLayers.Add(AnimationStance.TwoHanded, anim.GetLayerIndex("Two Handed"));
        equipmentStanceLayers.Add(AnimationStance.OneHandedShield, anim.GetLayerIndex("One Handed Shield"));
    }

    public void GoToNextPrimaryEquipment()
    {
        if (availablePrimaryEquipment.Count > 0)
        {
            primaryIndex++;
            if (primaryIndex >= availablePrimaryEquipment.Count)
            {
                primaryIndex = 0;
            }
            // Unequip old equipment
            availablePrimaryEquipment.Add(primarySlot.UnEquip());

            primarySlot.Equip(availablePrimaryEquipment[primaryIndex]);
            if (primarySlot.CurrentlyEquipped.usage == Equipment.Usage.Both)
            {
                availableSecondaryEquipment.Add(secondarySlot.UnEquip());
            }
        }
        UpdateCurrentEquipmentStance();
    }

    public void GoToNextSecondaryEquipment()
    {
        if (primarySlot.CurrentlyEquipped?.usage != Equipment.Usage.Both)
        {
            if (availableSecondaryEquipment.Count > 0)
            {
                secondaryIndex++;
                if (secondaryIndex >= availableSecondaryEquipment.Count)
                {
                    secondaryIndex = 0;
                }

                availableSecondaryEquipment.Add(secondarySlot.UnEquip());
                secondarySlot.Equip(availableSecondaryEquipment[secondaryIndex]);
            }
        }
        UpdateCurrentEquipmentStance();
    }

    public void UpdateCurrentEquipmentStance()
    {
        if (primarySlot.CurrentlyEquipped.usage == Equipment.Usage.Both)
        {
            CurrentStance = AnimationStance.TwoHanded;
        }
        else
        {
            CurrentStance = AnimationStance.OneHandedShield;
        }
        // set all stance layers to 0 weight
        foreach (var stance in equipmentStanceLayers)
        {
            anim.SetLayerWeight(stance.Value, 0);
        }
        anim.SetLayerWeight(equipmentStanceLayers[CurrentStance], 1);
    }
}
