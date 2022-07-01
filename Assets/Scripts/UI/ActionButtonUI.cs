using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button button;
    [SerializeField] private GameObject selectedGameObject;
    private BaseAction action;

    public void SetBaseAction(BaseAction action)
    {
        this.action = action;
        text.text = action.GetActionName();
        button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(action);
        });
    }

    public void UpdateSelectedVisual()
    {
        selectedGameObject.SetActive(action == UnitActionSystem.Instance.GetSelectedAction());
    }
}