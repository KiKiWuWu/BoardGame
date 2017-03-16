using System;
using System.Collections.Generic;
using UnityEngine;

public class AllUnitsController : MonoBehaviour {
    public CellGrid cellGrid;

    private List<Unit> listOfPlayerOneUnits = new List<Unit>();
    private List<Unit> listOfPlayerTwoUnits = new List<Unit>();
    private Unit selectedAlliedUnit;
    private Unit selectedEnemyUnit;
    
    private int currentActivePlayer;
    private bool enemyIsBeingAttacked = false;

    private CursorOverPlayerController cursorController;


    // Use this for initialization
    void Start () {
        cursorController = gameObject.GetComponent<CursorOverPlayerController>();

        currentActivePlayer = cellGrid.CurrentPlayerNumber;
	}

    /*
    //Gets all units (foe and enemy) which are placed on the field
    private void getListOfAllUnitsOnTheField()
    {
        print(cellGrid.Units.Count);
        for(int i = 0; i < cellGrid.Units.Count; i++)
        {
            if(cellGrid.Units[i].PlayerNumber == 0)
            {
                listOfPlayerOneUnits.Add(cellGrid.Units[i]);
            }
            else
            {
                listOfPlayerTwoUnits.Add(cellGrid.Units[i]);
            }
        }
    }

    //Updates the list of units of all players
    public void updateUnitListOfAllPlayer()
    {
        listOfPlayerOneUnits.Clear();
        listOfPlayerTwoUnits.Clear();

        getListOfAllUnitsOnTheField();
    }
    */

    //Changes the current active player when a turn is changed
    public void changeCurrentActivePlayer()
    {
        currentActivePlayer = cellGrid.CurrentPlayerNumber;
    }


    //Returns the current active player (called by GUIController and Unit class)
    public int activePlayer()
    {
        return currentActivePlayer;
    }


    //Saves the by the player selected allied unit 
    public void selectedAlliedUnitByPlayer(Unit selectedUnit)
    {
        selectedAlliedUnit = selectedUnit;

        if (selectedUnit != null)
        {
            cursorController.showCursorOverSelectedUnit("friend", selectedUnit);
        }
        else
        {
            cursorController.hidePlayerCursor("friend");
        }
    }


    //Saves the by the player selected enemy unit 
    public void selectedEnemyUnitByPlayer(Unit selectedUnit)
    {
        selectedEnemyUnit = selectedUnit;

        if (selectedUnit != null)
        {
            cursorController.showCursorOverSelectedUnit("enemy", selectedUnit);
        }
        else
        {
            cursorController.hidePlayerCursor("enemy");
        }
    }


    //Returns the currenty selected allied unit (called by GUIController class)
    public Unit currentlySelectedAlliedUnit()
    {
        return selectedAlliedUnit;
    }


    //Returns the currenty selected enemy unit ()
    public Unit currentlySelectedEnemyUnit()
    {
        return selectedEnemyUnit;
    }


    //Unlocks the special attack of the selected Unit
    public void unlockSpecialAttackOfSelectedUnit()
    {
        selectedAlliedUnit.specialAttackPurchased = true;
    }


    //Returns true or false if a attack is currently performed or not
    public bool isAttackCurrentlyPerformed()
    {
        return enemyIsBeingAttacked;
    }


    //Changes the state if a attack is currently performed
    public void stateOnAttack(bool attackState)
    {
        enemyIsBeingAttacked = attackState;
    }
}