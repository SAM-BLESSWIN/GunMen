using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{
    [SerializeField] private GameObject actionBusyGameObject;

    private void Start()
    {
        UnitActionSystem.Instance.OnBusyChanged += UnitActionSystem_OnBusyChanged;
        UpdateBusyState(false);
    }

    private void UnitActionSystem_OnBusyChanged(object sender, bool isBusy)
    {
        UpdateBusyState(isBusy);
    }

    private void UpdateBusyState(bool isBusy)
    {
        actionBusyGameObject.SetActive(isBusy);
    }
}
