using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }

    private int turnNumber = 1;
    public int TurnNumber { get { return turnNumber; } }

    public event EventHandler OnTurnChanged;

    private void Awake()
    {
        //SINGLETON PATTERN
        if (Instance != null)
        {
            Debug.LogError("There is more than one TurnSystem!" + transform + " - " + Instance);
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void NextTurn()
    {
        turnNumber++;
        OnTurnChanged?.Invoke(this,EventArgs.Empty);
    }
}
