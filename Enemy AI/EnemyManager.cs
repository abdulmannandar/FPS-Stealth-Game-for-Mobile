using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private bool isCombatTriggered = false; 

    public void TriggerInvestigation()
    {
        if (!isCombatTriggered)
        {
            isCombatTriggered = true;
            NotifyAllEnemiesToCombat();
        }
    }

    private void NotifyAllEnemiesToCombat()
    {
        
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in allEnemies)
        {
            enemy.Investigate();
        }
    }
}
