using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class AgentHealth : NetworkBehaviour
{
    public float maxHealth = 100f;

    public float CurrentHealth { get { return currentHealth; } }

    [SyncVar]
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    [ClientRpc]
    public void RPC_Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            CMD_Kill();
        }
    }

    [TargetRpc]
    public void Target_DamageNotification(float damage)
    {
        Debug.Log("You just received " + damage + " damage!");
    }

    [ClientRpc]
    public void Heal(float health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        print(currentHealth);
    }

    [Command]
    public void CMD_Kill()
    {
        RPC_Kill();
    }

    [ClientRpc]
    public void RPC_Kill()
    {
        gameObject.SetActive(false);
    }
}
