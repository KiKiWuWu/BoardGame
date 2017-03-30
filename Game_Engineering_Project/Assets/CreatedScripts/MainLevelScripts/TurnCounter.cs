using UnityEngine;

public class TurnCounter : MonoBehaviour
{

    private int turnCounter = 1;


    //Adds 1 to the turn counter after a player finishes the turn (called by GUIController class)
    public void changeTurn()
    {
        turnCounter++;
    }


    //Returns the current turn count
    public int currentTurn()
    {
        return turnCounter;
    }
}