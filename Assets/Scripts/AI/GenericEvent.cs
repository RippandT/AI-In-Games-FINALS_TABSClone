using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEvent : MonoBehaviour
{
    public UnitManager unitManager;

    void Start()
    {
        unitManager = GetComponentInParent<UnitManager>();
        Debug.Log(unitManager);
    }

    void Update()
    {
        
    }
}
