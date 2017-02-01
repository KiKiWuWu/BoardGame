using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIControllerHexa : MonoBehaviour
{
    public CellGrid CellGrid;
    public GameObject ChangeTurnScreen;
    public GameObject EndScreen;
    public GameObject BuffScreen;
    public Text EndScreenText;
    public bool buffActivated = false;

    private BuffSpawner _buffSpawner = new BuffSpawner();

    private ActionCount actionsCounter = new ActionCount();

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
        actionsCounter.showCount();
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


    public void executeBuff()
    {
        if (!buffActivated)
        {
            List<Unit> listWithUnitsToBuff = new List<Unit>();
            listWithUnitsToBuff = GameObject.FindGameObjectWithTag("attackBuff").GetComponent<TestCollider>().getUnitsForBuff();

            if (listWithUnitsToBuff.Count != 0)
            {
                for (int i = 0; i < listWithUnitsToBuff.Count; i++)
                {
                    _buffSpawner.SpawnBuff(new AttackBuff(0, 10), listWithUnitsToBuff[i]);
                }
            }

            BuffScreen.SetActive(true);
            Invoke("hideBuffScreenInformation", 1);

            buffActivated = !buffActivated;
        }
    }


    private void hideBuffScreenInformation()
    {
        BuffScreen.SetActive(false);
    }
}
