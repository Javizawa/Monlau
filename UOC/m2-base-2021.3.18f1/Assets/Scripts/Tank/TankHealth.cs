using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class TankHealth : NetworkBehaviour
{
    public const int maxHealth = 100;
    public Vector3 spawnPoint;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;
    public GameObject tank;
    public GameObject tankHealth;
    public int tL;



    // Update is called once per frame
    void Update()
    {
        CheckDeath();
    }

    public void TakeDamage(int amount, string tag)
    {
        if (!isServer)
        {  
            return;
        }
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (tag == "Tank") {
                if(tL > 0) {
                    tL--;
                    currentHealth = maxHealth;
                    RpcRespawn();
                }
            }
        }

        OnChangeHealth(currentHealth,3);
    }

    private void OnChangeHealth(int newHealth, int test)
    {
        tankHealth.GetComponent<RectTransform>().sizeDelta = new Vector2(currentHealth, 10);
    }


    void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(tank);
        }
    }


    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = spawnPoint;
        }
    }


}
