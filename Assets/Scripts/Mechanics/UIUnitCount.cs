using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIUnitCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI allianceCounter;
    [SerializeField] TextMeshProUGUI enterpriseCounter;
    [SerializeField] GameObject allianceVictoryScreen;
    [SerializeField] GameObject enterpriseVictoryScreen;

    int allianceCount;
    int enterpriseCount;

    void Start()
    {
        allianceCount = GameObject.FindGameObjectsWithTag("AnonymousAlliance").Length;
        enterpriseCount = GameObject.FindGameObjectsWithTag("EnormousEnterprise").Length;

        allianceCounter.text = allianceCount.ToString();
        enterpriseCounter.text = enterpriseCount.ToString();
    }

    public void UpdateCount(int increment, int teamAffliation)
    {
        switch (teamAffliation)
        {
            case 0:
                enterpriseCount += increment;
                enterpriseCounter.text = enterpriseCount.ToString();
                break;
            case 1:
                allianceCount += increment;
                allianceCounter.text = allianceCount.ToString();
                break;
        }

        if (allianceCount <= 0)
        {
            enterpriseVictoryScreen.SetActive(true);
            gameObject.SetActive(false);
        }
        if (enterpriseCount <= 0)
        {
            allianceVictoryScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
