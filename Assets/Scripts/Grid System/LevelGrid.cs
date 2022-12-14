using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    [SerializeField] private GameObject gridDebugObjectPrefab;
    [SerializeField] private Transform gridObjectParent;
    private GridSystem gridSystem;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than OneLevelGrid!" + transform + " - " + Instance);
            Destroy(Instance);
        }
        Instance = this;

        gridSystem = new GridSystem(10, 10, 2f);
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab, gridObjectParent);
    }

    public void AddUnitAtGridPosition(GridPosition gridPosition,Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }

    public List<Unit> GetUnitsAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition,Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    public void UpdateUnitGridPosition(GridPosition fromGridPosition,GridPosition toGridPosition, Unit unit)
    {
        RemoveUnitAtGridPosition(fromGridPosition,unit);
        AddUnitAtGridPosition(toGridPosition,unit);
    }

    //Passthrough function : calling a function in hidden class with a function in exposed class
    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);
    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);
    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);
    public int GetWidth() => gridSystem.Width;
    public int GetHeight() => gridSystem.Height;

    public GridSystem GetGridSystem() => gridSystem;

    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.HasAnyUnit();
    }

}
