using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Unit_Types
{
    Brawler,
    Melee,
    Gunner,
    Manager
}

public enum Team
{
    EnormousEnterprise,
    AnonymousAlliance
}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class UnitManager : MonoBehaviour
{
    public string[] enemyTeam;
    public Dictionary<Unit_Types,int> unitIndices = new Dictionary<Unit_Types, int>()
    {
        {Unit_Types.Brawler, 0},
        {Unit_Types.Melee, 1},
        {Unit_Types.Gunner, 2},
        {Unit_Types.Manager, 3}
    };

    [Header("Unit Information")]
    public float health;
    public float damage;
    public float speed;
    public Team teamAffliation;
    public int returnTeamAffliation;
    public Unit_Types unitType;

    [Header("State Machine")]
    public string currentState;

    [Header("References")]
    public List<GameObject> unitModels;
    public Animator animator;
    public NavMeshAgent agent;

    [Header("Attack Range")]
    public float range;

    [Header("Melee Hitbox")]
    public GameObject hitbox;

    [Header("Ranged Unit Ammunition")]
    public GameObject projectile;
    public Transform projectileSpawn;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        returnTeamAffliation = (int)teamAffliation;

        Action<Unit_Types> activateUnit = (Unit_Types unitType) =>
        {/*
            foreach (var unit in unitModels)
            {
                if (unit == unitModels[unitIndices[unitType]])
                {
                    GameObject unitToActivate = unit;
                    unitToActivate.SetActive(true);
                    animator = unitToActivate.GetComponent<Animator>();
                }
                else
                {
                    Destroy(unit);
                }
            }*/
            GameObject unitToActivate = unitModels[unitIndices[unitType]];
            unitToActivate.SetActive(true);
            animator = unitToActivate.GetComponent<Animator>();
        };

        activateUnit(unitType);
        SetUnitProperties();
        agent.speed = speed;
    }

    void LateUpdate()
    {
        // Check if animator has a reference
        if (animator == null)
        {
            currentState = "Animator reference not set";
            return;
        }

        AnimationClip currentClip = GetCurrentAnimatorClip(animator, 0);

        // Check if there's animation
        if (currentClip == null)
        {
            currentState = "No animation playing";
            return;
        }

        currentState = currentClip.name;
    }

    void SetUnitProperties()
    {
        switch(unitType)
        {
            case Unit_Types.Brawler:
                health = GameManager.Instance.brawlerHealth;
                damage = GameManager.Instance.brawlerDamage;
                speed = GameManager.Instance.brawlerSpeed;
                range = GameManager.Instance.brawlerRange;
                break;
            case Unit_Types.Melee:
                health = GameManager.Instance.meleeHealth;
                damage = GameManager.Instance.meleeDamage;
                speed = GameManager.Instance.meleeSpeed;
                range = GameManager.Instance.meleeRange;
                break;
            case Unit_Types.Gunner:
                health = GameManager.Instance.gunnerHealth;
                damage = GameManager.Instance.gunnerDamage;
                speed = GameManager.Instance.gunnerSpeed;
                range = GameManager.Instance.gunnerRange;
                break;
            case Unit_Types.Manager:
                health = GameManager.Instance.managerHealth;
                damage = GameManager.Instance.managerDamage;
                speed = GameManager.Instance.managerSpeed;
                range = GameManager.Instance.managerRange;
                break;
        }
    }

    private AnimationClip GetCurrentAnimatorClip(Animator anim, int layer)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(layer);
        return anim.GetCurrentAnimatorClipInfo(layer)[0].clip;
    }
}
