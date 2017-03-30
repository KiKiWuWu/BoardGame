using System.Collections.Generic;
using UnityEngine;

public class AllUnitsController : MonoBehaviour
{
    public CellGrid cellGrid;
    
    private Unit selectedAlliedUnit;
    private Unit selectedEnemyUnit;
    
    private int currentActivePlayer;
    private int maxNumberOfUnits = 6;
    private int specialAttackBonusAttack = 4;
    private bool enemyIsBeingAttacked = false;

    private CursorOverPlayerController cursorController;


    // Use this for initialization
    void Start () {
        cursorController = gameObject.GetComponent<CursorOverPlayerController>();

        currentActivePlayer = cellGrid.CurrentPlayerNumber;
	}

    //Creates a list with all characters of the current player and returns then (called by UnitOverviwController.cs)
    public List<Unit> getCurrentPlayerUnits(int playersUnits)
    {
        List<Unit> listWithUnits = new List<Unit>();
        var allUnits = GameObject.Find("Unit");

        for (int i = 0; i < allUnits.transform.childCount; i++)
        {
            if (allUnits.transform.GetChild(i).GetComponent<Unit>().PlayerNumber == playersUnits)
            {
                listWithUnits.Add(allUnits.transform.GetChild(i).GetComponent<Unit>());
            }
        }
        return listWithUnits;
    }


    //Returns the maximum number of units
    public int numberOfUnits()
    {
        return maxNumberOfUnits;
    }


    //Changes the current active player when a turn is changed
    public void changeCurrentActivePlayer()
    {
        currentActivePlayer = cellGrid.CurrentPlayerNumber;
    }


    //Returns the current active player (called by GUIControllerHexa.cs and Unit.cs class)
    public int activePlayer()
    {
        return currentActivePlayer;
    }


    //Saves the by the current player selected allied unit 
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


    //Saves the by the current player selected enemy unit 
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


    //Returns the currenty selected allied unit (called by GUIControllerHexa.cs)
    public Unit currentlySelectedAlliedUnit()
    {
        return selectedAlliedUnit;
    }


    //Returns the currenty selected enemy unit
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


    //Sets the currently selected unit on a finished state and deselects this unit (called by CastleController.cs and BuffExecuter.cs)
    public void setAlliedUnitToFinishedState()
    {
        currentlySelectedAlliedUnit().setUnitToFinishState();
        currentlySelectedAlliedUnit().OnUnitDeselected();
    }


    //Returns the additional attack points when special attack of unit is used
    public int getAdditionalAttackPointsWhenSpecialAttack()
    {
        return specialAttackBonusAttack;
    }
}