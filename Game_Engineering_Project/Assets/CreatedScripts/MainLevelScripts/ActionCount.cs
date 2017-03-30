using UnityEngine;

public class ActionCount : MonoBehaviour
{
    private static int actionPointsEveryTurn = 20;
    private int currentlyAvailableActionPoints;

    private int costOfAttack = 5;
    private int costOfActivatingSpecialAbilityAttack = 10;
    private int costOfActivatingBuff = 3;
    private int costToNeutralizeOrOccupieCastle = 5;
    private int costToTakeDefencePosition = 2;
    private int additionalActionPointsFromPowerUp = 5;

    private GUIControllerHexa guiController;
    private CharacterSpecialAttackController specialAttackController;


    //Function is called on start
    private void Start()
    {
        guiController = gameObject.GetComponent<GUIControllerHexa>();
        specialAttackController = gameObject.GetComponent<CharacterSpecialAttackController>();

        currentlyAvailableActionPoints = actionPointsEveryTurn;
    }


    //Reduces the current action points count depending on the amount
    public void subtractCostOfActionFromCurrentActionCount(string command)
    {
        if (command == "attack")
        {
            int tempAttackCount;
            if (specialAttackController.specialAttackActivatedByUser())
            {
                tempAttackCount = costOfActivatingSpecialAbilityAttack;
            }
            else
            {
                tempAttackCount = costOfAttack;
            }

            currentlyAvailableActionPoints -= tempAttackCount;
        }
        if (command == "buff")
        {
            currentlyAvailableActionPoints -= costOfActivatingBuff;
        }
        if (command == "castle")
        {
            currentlyAvailableActionPoints -= costToNeutralizeOrOccupieCastle;
        }
        if(command == "defencePosition")
        {
            currentlyAvailableActionPoints -= costToTakeDefencePosition;
        }
    }


    //Restarts the remaining action count when called (called by GUIControllerHexa.cs)
    public void restartAvailableActionPoints()
    {
        currentlyAvailableActionPoints = actionPointsEveryTurn;
    }


    //Returns the current action points of the current player
    public int getCountOfRemainingActions()
    {
        return currentlyAvailableActionPoints;
    }


    //Checks if a attack with the remaining action points is possible (called by Unit.cs)
    public bool checkIfAttackIsPossible()
    {
        if (specialAttackController.specialAttackActivatedByUser() && currentlyAvailableActionPoints >= costOfActivatingSpecialAbilityAttack)
        {
            return true;
        }
        else if (!specialAttackController.specialAttackActivatedByUser() && currentlyAvailableActionPoints >= costOfAttack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //Reduces the current action counter by the number of moved fields (called by Unit.cs)
    public void subtractMovementCostFromCurrentActionCount(int totalMovementCost)
    {
        currentlyAvailableActionPoints -= totalMovementCost;
    }


    //Checks if there are enought remaining action points to activate a special attack (called by CharacterSpecialAttackController.cs)
    public bool checkRemainingPointsForSpecialAttack()
    {
        if (currentlyAvailableActionPoints >= costOfActivatingSpecialAbilityAttack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //Checks if there are enought remaining action points to activate a buff (called by GUIControllerHexa.cs)
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


    //Checks if it is possible to occupie or neutralize castle (called by CastleController.cs)
    public bool castleNeutralizeOrOccupiePossible()
    {
        if(currentlyAvailableActionPoints >= costToNeutralizeOrOccupieCastle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //Returns the total number of action points
    public int totalNumberOfActionPoints()
    {
        return actionPointsEveryTurn;
    }


    //Returns the number of action points which are necessery to activate a buff
    public int numberOfActionPointsToActivateBuff()
    {
        return costOfActivatingBuff;
    }


    //Returns the number of action points which are neceseery to occupie/neutralize castle (called by MessageOnScreenController.cs)
    public int getPointsForOccupyingCastle()
    {
        return costToNeutralizeOrOccupieCastle;
    }


    //Checks if the player has enough action points to take the defence position 
    public bool checkIfTakingDefPositionIsPossible()
    {
        if(currentlyAvailableActionPoints >= costToTakeDefencePosition)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //Adds extra action points to the current player action points count
    public void addActionPointsForCurrentPlayer()
    {
        currentlyAvailableActionPoints += additionalActionPointsFromPowerUp;
    }
}