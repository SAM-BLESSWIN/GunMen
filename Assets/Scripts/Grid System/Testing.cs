using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private GameObject gridDebugObjectPrefab;
    [SerializeField] private Transform gridObjectParent;
    private GridSystem gridSystem;

    void Start()
    {
        gridSystem = new GridSystem(10, 10, 2f);
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab,gridObjectParent);
    }

    private void Update()
    {
        Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetMouseWorldPosition()));
    }

}
