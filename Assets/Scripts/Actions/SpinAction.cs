using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float totalSpinAmount = 0f;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if(!isActive) { return; }

        float rotateSpeed = 90f;
        float spinAddAmount = rotateSpeed * Time.deltaTime;
        transform.eulerAngles += Vector3.up * spinAddAmount;
        totalSpinAmount += spinAddAmount;

        if(totalSpinAmount >= 360f)
        {
            isActive = false;
            onActionComplete();
        }
    }

    public override string GetActionName()
    {
        return "Spin";
    }

    public void foo()
    {
        Debug.Log("foo");
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        totalSpinAmount = 0f;
        isActive = true;
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        GridPosition unitGridPosition = unit.GetUnitGridPosition();
        return new List<GridPosition> { unitGridPosition};
    }
}
