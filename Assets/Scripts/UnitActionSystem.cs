using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; } 

    public event EventHandler OnUnitSelectedChanged;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitsLayerMask;

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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TrySelectUnit()) return;

            selectedUnit.Move(MouseWorld.GetMouseWorldPosition());
        }
    }

    private bool TrySelectUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit hitInfo,Mathf.Infinity,unitsLayerMask))
        {
            if(hitInfo.transform.gameObject.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;

        //OBSERVER PATTERN
        OnUnitSelectedChanged?.Invoke(this, EventArgs.Empty); //publisher
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
