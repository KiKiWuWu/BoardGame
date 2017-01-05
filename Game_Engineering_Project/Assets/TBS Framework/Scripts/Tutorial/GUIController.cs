using System;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    public CellGrid CellGrid;
    public GameObject ChangeTurnScreen;
    public GameObject EndScreen;
    public Text EndScreenText;

    void Start()
    {
        CellGrid.TurnEnded += OnTurnEnded;
        CellGrid.GameEnded += OnGameEnded;
    }

    public void quitAllpication()
    {
        Application.Quit();
    }

    public void finishTurn()
    {
        CellGrid.EndTurn();
    }


    private void ShowOrHideNextTurnMessege()
    {
        ChangeTurnScreen.SetActive(false);
    }


    private void OnTurnEnded(object sender, EventArgs e)
    {
        if((sender as CellGrid).CurrentPlayer is HumanPlayer)
        {
            ChangeTurnScreen.SetActive(true);

            Invoke("ShowOrHideNextTurnMessege", 1);
        }
    }


    private void OnGameEnded(object sender, EventArgs e)
    {
        if((sender as CellGrid).CurrentPlayerNumber == 0)
        {
            EndScreenText.text = "Glückwunsch Sie haben gewonnen!";
        }
        else
        {
            EndScreenText.text = "Leider haben Sie verloren :(";
        }
        EndScreen.SetActive(true);
    }


}
