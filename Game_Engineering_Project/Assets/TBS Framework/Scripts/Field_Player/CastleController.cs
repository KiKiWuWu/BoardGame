using System.Collections.Generic;
using UnityEngine;

public class CastleController : MonoBehaviour {

    public GameObject AllCastleObjects;

    private AllUnitsController unitController;
    private ActionCount actionCount;
    private GUIControllerHexa guiController;

    private List<MyCastle> castleList;


    // Use this for initialization
    void Start () {
        unitController = gameObject.GetComponent<AllUnitsController>();
        actionCount = gameObject.GetComponent<ActionCount>();
        guiController = gameObject.GetComponent<GUIControllerHexa>();

        castleList = new List<MyCastle>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    //Counts the number of occupied castles which the current player posseses
    public int countOccupiedCastlesOfCurrentPlayer(int currentPlayer)
    {
        updateCurrentListOfCastles();
        int numbOfOccupiedCastles = 0;

        for(int i = 0; i < castleList.Count; i++)
        {
            if(castleList[i].castleOccupation() == currentPlayer)
            {
                numbOfOccupiedCastles++;
            }
        }
        return numbOfOccupiedCastles;
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


    //Changes the occupation state of the castle on which a unit is curretly standing to neutral
    public void neutralizeCastle()
    {
        if (actionCount.castleNeutralizeOrOccupiePossible())
        {
            actionCount.subtractCostOfActionFromCurrentActionCount("castle");
            unitController.currentlySelectedAlliedUnit().standOnCastle.changeStateOfCastle(-1);

            unitController.currentlySelectedAlliedUnit().setUnitToFinishState();

            guiController.showMessageOnScreenAboutCastleState("castleNeutralized");
        }
        else
        {
            guiController.showMessageOnScreenAboutCastleState("notEnoughtPoints");
        }
    }


    //Changes the occupation state of the castle on which a unit is curretly standing to the fraction which the character belongs to
    public void occupieCastle()
    {
        if (actionCount.castleNeutralizeOrOccupiePossible())
        {
            actionCount.subtractCostOfActionFromCurrentActionCount("castle");
            unitController.currentlySelectedAlliedUnit().standOnCastle.changeStateOfCastle(unitController.activePlayer());
            
            unitController.currentlySelectedAlliedUnit().setUnitToFinishState();

            guiController.showMessageOnScreenAboutCastleState("castleOccupied");
        }
        else
        {
            guiController.showMessageOnScreenAboutCastleState("notEnoughtPoints");
        }
    }
}
