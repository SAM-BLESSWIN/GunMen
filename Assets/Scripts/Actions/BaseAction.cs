using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract : No Instance of this class//
public abstract class BaseAction : MonoBehaviour
{
    protected Action onActionComplete; //Inbuilt Delegate

    protected Unit unit;
    protected bool isActive;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }
}
