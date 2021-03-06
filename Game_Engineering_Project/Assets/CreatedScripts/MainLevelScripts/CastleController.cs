﻿using System.Collections.Generic;
using UnityEngine;

public class CastleController : MonoBehaviour
{
    public GameObject AllCastleObjects;

    private AllUnitsController unitController;
    private ActionCount actionCount;
    private MessagesOnScreenController messageOnScreenController;

    private List<MyCastle> castleList;


    // Use this for initialization
    void Start () {
        unitController = gameObject.GetComponent<AllUnitsController>();
        actionCount = gameObject.GetComponent<ActionCount>();
        messageOnScreenController = gameObject.GetComponent<MessagesOnScreenController>();

        castleList = new List<MyCastle>();
    }


    //Counts the number of occupied castles which the current player is controlling
    public int countOccupiedCastlesOfCurrentPlayer(int currentPlayer)
    {
        if(AllCastleObjects != null)
        {
            updateCurrentListOfCastles();
            int numbOfOccupiedCastles = 0;

            for (int i = 0; i < castleList.Count; i++)
            {
                if (castleList[i].castleOccupation() == currentPlayer)
                {
                    numbOfOccupiedCastles++;
                }
            }
            return numbOfOccupiedCastles;
        }
        else
        {
            return 0;
        }
    }


    //Gets the current list of castles
    private void updateCurrentListOfCastles()
    {
        if(castleList.Count != 0)
        {
            castleList.Clear();
        }

        for (int i = 0; i < AllCastleObjects.transform.childCount; i++)
        {
            var castle = AllCastleObjects.transform.GetChild(i).GetComponent<MyCastle>();
            castleList.Add(castle);
        }
    }


    //Checks if the occupation of the castle is possible and displays a message on the screen
    public void checkIfOccupationOfCastleIsPossible()
    {
        if (actionCount.castleNeutralizeOrOccupiePossible())
        {
            messageOnScreenController.showOccupationOrNeutralizationScreen("occupation");
        }
        else
        {
            messageOnScreenController.showMessageOnScreenAboutCastleState("notEnoughtPoints");
        }
    }


    //Checks if the neutralization of the castle is possible and displays a message on the screen
    public void checkIfNeutralizationOfCastleIsPossible()
    {
        if (actionCount.castleNeutralizeOrOccupiePossible())
        {
            messageOnScreenController.showOccupationOrNeutralizationScreen("neutralization");
        }
        else
        {
            messageOnScreenController.showMessageOnScreenAboutCastleState("notEnoughtPoints");
        }
    }


    //Changes the occupation state of the castle on which a unit is curretly standing to the fraction which the character belongs to
    public void occupieCastle()
    {
        actionCount.subtractCostOfActionFromCurrentActionCount("castle");
        unitController.currentlySelectedAlliedUnit().standOnCastle.changeStateOfCastle(unitController.activePlayer());

        unitController.setAlliedUnitToFinishedState();

        messageOnScreenController.showMessageOnScreenAboutCastleState("castleOccupied");

    }


    //Changes the occupation state of the castle on which a unit is curretly standing to neutral
    public void neutralizeCastle()
    {
        actionCount.subtractCostOfActionFromCurrentActionCount("castle");
        unitController.currentlySelectedAlliedUnit().standOnCastle.changeStateOfCastle(-1);

        unitController.setAlliedUnitToFinishedState();

        messageOnScreenController.showMessageOnScreenAboutCastleState("castleNeutralized");
    }


    //Destroys all castles which are on the field when the death game is starting
    public void destroyAllCastlesOnTheField()
    {
        Destroy(AllCastleObjects);
        AllCastleObjects = null;
    }
}