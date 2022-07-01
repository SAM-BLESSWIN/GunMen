using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private GridPosition gridPosition;
    private MoveAction moveAction;
    private SpinAction spinAction;
    private BaseAction[] baseActions;

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActions = GetComponentsInChildren<BaseAction>();
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition,this);
    }

    private void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridPosition != gridPosition)
        {
            LevelGrid.Instance.UpdateUnitGridPosition(gridPosition, newGridPosition, this);
            gridPosition = newGridPosition;
        }
    }

    public MoveAction GetMoveAction() => moveAction;
    public SpinAction GetSpinAction() => spinAction;
    public BaseAction[] GetBaseActions() => baseActions;
    public GridPosition GetUnitGridPosition() => gridPosition;

}
