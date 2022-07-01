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

    /*Abstract Method : makes it compulsory to implement in the derived class*/
    public abstract string GetActionName();
    public abstract void TakeAction(GridPosition gridPosition,Action onActionComplete);

    public virtual bool IsValidActionGridPosition(GridPosition targetGridPosition)
    {
        List<GridPosition> validGridPositions = GetValidActionGridPositionList();
        return validGridPositions.Contains(targetGridPosition);
    }

    public abstract List<GridPosition> GetValidActionGridPositionList();
}
