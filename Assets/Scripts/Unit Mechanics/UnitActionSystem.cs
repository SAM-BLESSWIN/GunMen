using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; } 

    public event EventHandler OnUnitSelectedChanged;  //Inbuilt Delegate
    public event EventHandler OnActionSelectedChanged;
    public event EventHandler<bool> OnBusyChanged;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitsLayerMask;

    private BaseAction selectedAction;
    private bool isBusy;
    
    private void Awake()
    {
        //SINGLETON PATTERN
        if(Instance != null)
        {
            Debug.LogError("There is more than OneActionSystem!" + transform + " - " + Instance);
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        SetSelectedAction(selectedUnit.GetMoveAction());
    }

    private void Update()
    {
        if(isBusy) { return; }

        if(EventSystem.current.IsPointerOverGameObject()) { return; }  //check whether cursor is over UI element

        if (Input.GetMouseButtonDown(0))
        {
            if (TrySelectUnit()) return;

            HandleSelectedAction();
        }

    }

    private void HandleSelectedAction()
    {
        GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMouseWorldPosition());

        if (selectedAction.IsValidActionGridPosition(mouseGridPosition))
        {
            SetBusy();
            selectedAction.TakeAction(mouseGridPosition, ClearBusy);
        }
    }

    private void SetBusy()
    {
        isBusy = true;
        OnBusyChanged?.Invoke(this, isBusy);
    }

    private void ClearBusy()
    {
        isBusy = false;
        OnBusyChanged?.Invoke(this, isBusy);
    }


    private bool TrySelectUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit hitInfo,Mathf.Infinity,unitsLayerMask))
        {
            if(hitInfo.transform.gameObject.TryGetComponent<Unit>(out Unit unit))
            {
                if (selectedUnit == unit) return false;

                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(selectedUnit.GetMoveAction());

        //OBSERVER PATTERN
        OnUnitSelectedChanged?.Invoke(this, EventArgs.Empty); //publisher
    }

    public void SetSelectedAction(BaseAction action)
    {
        if(selectedAction == action) return;

        selectedAction = action;
        OnActionSelectedChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit() => selectedUnit;
    public BaseAction GetSelectedAction() => selectedAction;

}
