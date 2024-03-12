using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    UnitManager unitManager;
    public List<GameObject> enemies;

    void Start()
    {
        unitManager = GetComponentInParent<UnitManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(unitManager.enemyTeam[unitManager.returnTeamAffliation]))
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemies.Remove(other.gameObject);
    }

    public void OnHit()
    {
        Debug.Log(enemies);
        Debug.Log(unitManager.damage);
    }
}
