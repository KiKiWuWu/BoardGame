using UnityEngine;

public class ActionCount : MonoBehaviour
{
    private static int actionPointsEveryTurn = 17;
    private int currentlyAvailableActionPoints;

    private int costOfAttack = 5;
    private int costOfActivatingBuff = 3;
    //private int costOfActivatingSpecialAbility = 5;

    private GUIControllerHexa guiController;


    //Function is called on start
    private void Start()
    {
        guiController = gameObject.GetComponent<GUIControllerHexa>();

        currentlyAvailableActionPoints = actionPointsEveryTurn;
    }


    //Reduces the current action points count and sends the cost number to the GUIController
    public void subtractCostOfActionFromCurrentActionCount(string command)
    {
        if(command == "attack")
        {
            currentlyAvailableActionPoints -= costOfAttack;
            guiController.showCostsOnScreen(costOfAttack);
        }
        if(command == "buff")
        {
            currentlyAvailableActionPoints -= costOfActivatingBuff;
            guiController.showCostsOnScreen(costOfActivatingBuff);
        }
    }


    //Restarts the remaining action count when called (called by GUIControllerHexa class)
    public void restartAvailableActionPoints()
    {
        currentlyAvailableActionPoints = actionPointsEveryTurn;
    }


    //Returns the current action points of the current player
    public int getCountOfRemainingActions()
    {
        return currentlyAvailableActionPoints;
    }


    //Returns the number of current action points of the player (called by Unit class)
    public int remainingActionPoints()
    {
        return currentlyAvailableActionPoints;
    }


    //Checks if a attack with the remaining action points is possible (called by Unit class)
    public bool checkIfAttackIsPossible()
    {
        if(currentlyAvailableActionPoints >= costOfAttack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //Reduces the current action counter by the number of moved fields (called by Unit class)
    public void subtractMovementCostFromCurrentActionCount(int totalMovementCost)
    {
        currentlyAvailableActionPoints -= totalMovementCost;
        guiController.showCostsOnScreen(totalMovementCost);
    }


    //Checks if there are enought remaining action points to activate a buff (called by GUIControllerHexa class)
    public bool buffActivationPossible()
    {
        if(currentlyAvailableActionPoints >= costOfActivatingBuff)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
