using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance { get; private set; }

    [SerializeField] private GameObject gridSystemVisualPrefab;
    [SerializeField] private Transform gridSystemVisualParent;

    private GridSystemVisualSingle[,] gridSystemVisuals;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than One GridSytemVisual!" + transform + " - " + Instance);
            Destroy(Instance);
        }
        Instance = this;
    }

    private void Start()
    {
        gridSystemVisuals = new GridSystemVisualSingle[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];
        
        for(int x=0;x<LevelGrid.Instance.GetWidth();x++)
        {
            for(int z=0;z<LevelGrid.Instance.GetHeight();z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);
                gridSystemVisuals[x,z] = 
                    Instantiate(gridSystemVisualPrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), 
                    Quaternion.identity, gridSystemVisualParent).GetComponent<GridSystemVisualSingle>();
            }
        }
    }

    private void Update()
    {
        UpdateGridVisual();
    }

    public void HideAllGridPositions()
    {
        for(int x=0;x< LevelGrid.Instance.GetWidth();x++)
        {
            for(int z=0;z < LevelGrid.Instance.GetHeight();z++)
            {
                gridSystemVisuals[x, z].ActivateMeshRenderer(false);
            }
        }
    }

    public void ShowValidGridPositions(List<GridPosition> validGridPositions)
    {
        foreach(GridPosition gridPosition in validGridPositions)
        {
            gridSystemVisuals[gridPosition.x, gridPosition.z].ActivateMeshRenderer(true);
        }
    }

    public void UpdateGridVisual()
    {
        HideAllGridPositions();
        ShowValidGridPositions(UnitActionSystem.Instance.GetSelectedUnit().GetMoveAction().GetValidActionGridPositionList());
    }
}
