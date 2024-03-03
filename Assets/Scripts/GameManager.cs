using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Brawler Default Stats")]
    public float brawlerHealth;
    public float brawlerDamage;
    public float brawlerSpeed;
    public float brawlerRange;
    [Header("Melee Default Stats")]
    public float meleeHealth;
    public float meleeDamage;
    public float meleeSpeed;
    public float meleeRange;
    [Header("Gunner Default Stats")]
    public float gunnerHealth;
    public float gunnerDamage;
    public float gunnerSpeed;
    public float gunnerRange;
    [Header("Manager Default Stats")]
    public float managerHealth;
    public float managerDamage;
    public float managerSpeed;
    public float managerRange;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
