using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private GameObject unitActionButtonPrefab;
    [SerializeField] private Transform unitActionButtonContainer;

    private List<ActionButtonUI> actionButtonUIList;

    private void Awake()
    {
        actionButtonUIList = new List<ActionButtonUI>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnUnitSelectedChanged += UnitActionSystem_OnSelectedUnitChanged;

        UnitActionSystem.Instance.OnActionSelectedChanged += UnitActionSystem_OnActionSelectedChanged;

        CreateUnitActionButtons();
        UpdateSelectedVisual();
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        CreateUnitActionButtons();
    }

    private void CreateUnitActionButtons()
    {
        foreach(Transform child in unitActionButtonContainer.transform)
        {
            Destroy(child.gameObject);
        }

        actionButtonUIList.Clear();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        foreach (BaseAction action in selectedUnit.GetBaseActions())
        {
            GameObject button = Instantiate(unitActionButtonPrefab, unitActionButtonContainer);
            button.TryGetComponent<ActionButtonUI>(out ActionButtonUI actionButton);
            actionButtonUIList.Add(actionButton);
            actionButton.SetBaseAction(action);
        }
    }
    private void UnitActionSystem_OnActionSelectedChanged(object sender, EventArgs e)
    {
        UpdateSelectedVisual();
    }

    private void UpdateSelectedVisual()
    {
        foreach (ActionButtonUI actionButton in actionButtonUIList)
        {
            actionButton.UpdateSelectedVisual();
        }
    }
}
