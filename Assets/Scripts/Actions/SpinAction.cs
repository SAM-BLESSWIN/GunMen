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

    public void Spin(Action OnSpinComplete)
    {
        onActionComplete = OnSpinComplete;
        totalSpinAmount = 0f;
        isActive = true;
    }
}
