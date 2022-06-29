using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;
    private float cellsize;
    private GridObject[,] gridObjectArray;

    public int Width { get => width;}
    public int Height { get => height; }

    public GridSystem(int width,int height,float cellsize)
    {
        this.width = width;
        this.height = height;
        this.cellsize = cellsize;

        gridObjectArray = new GridObject[width,height];

        for(int x=0;x<width;x++)
        {
            for(int z=0;z<height;z++)
            {
                gridObjectArray[x,z] = new GridObject(this,new GridPosition(x,z)); 
            }
        }
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x,0,gridPosition.z) * cellsize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(Mathf.RoundToInt(worldPosition.x/cellsize),
                                Mathf.RoundToInt(worldPosition.z/cellsize));
    }

    public void CreateDebugObjects(GameObject debugPrefab,Transform objectParent)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);
                GameObject debugObject = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity, objectParent);
                debugObject.GetComponent<GridDebugObject>().SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < width && gridPosition.z >= 0 && gridPosition.z < height;
    }
}


