using System;
using UnityEngine;
using UnityEngine.UI;

public class GUIControllerHexa : MonoBehaviour
{
    public CellGrid CellGrid;

    public GameObject UITopLeft;
    public GameObject UITopRight;
    public GameObject Canvas;
    public GameObject ChangeTurnScreen;
    public GameObject EndScreen;
    public GameObject BuffScreen;
    public GameObject NotEnoughtActionPointsScreen;
    public GameObject ActivateBuffButtonOnScreen;
    public GameObject ActivateOrCancelBuffButtons;

    public Button BuffPurchaseButton;
    public Button ActivateBuffButton;

    public Text EndScreenText;
    public Text RemainingActionCountOnScreen;
    public Text PlayerGold;
    public Text DisplayCostForActionOnScreen;
    public Text HPPlayer;
    public Text HPEnemy;
    public Text RoundCounter;

    public Slider HPSliderPlayer;
    public Slider HPSliderEnemy;

    public Image ImagePlayer;
    public Image ImageEnemy;


    private ActionCount actionsCounter;
    private int turnCounter;
    private TrackableImageOnScreenHandler trackableHandler;
    private BuffExecuter buffExecuter;



    //This function is called on start, initiates classes and EventHandler
    void Start()
    {
        actionsCounter = gameObject.GetComponent<ActionCount>();
        trackableHandler = gameObject.GetComponent<TrackableImageOnScreenHandler>();
        buffExecuter = gameObject.GetComponent<BuffExecuter>();
        turnCounter = 1;
        CellGrid.TurnEnded += OnTurnEnded;
        CellGrid.GameEnded += OnGameEnded;
    }

    public void hidePlayerUI()
    {
        //PlayerUI.SetActive(false);
    }


    //Update function is called every frame
    void Update()
    {
        showOrHideCanvas();
        updateActionCountOnScreen();
        showOrHideBuffPurchaseButton();
        checkIfBuffPurchaseIsPossible();
        showOrHideBuffActivateOrCancelButtons();
        checkIfUnitsAreInBuffArea();
    }


    //Enables or disables the interactability of the buff activate button 
    private void checkIfUnitsAreInBuffArea()
    {
        if (buffExecuter.areUnitsInBuffArea())
        {
            ActivateBuffButton.interactable = true;
        }
        else
        {
            ActivateBuffButton.interactable = false;
        }
    }

    

    //Shows or hides the activate/cancel buttons of a buff
    private void showOrHideBuffActivateOrCancelButtons()
    {
        if (trackableHandler.buffPurchased())
        {
            ShowOrHideBuffActivateOrCancelButtons(true);
        }
        else
        {
            ShowOrHideBuffActivateOrCancelButtons(false);
        }
    }


    //Shows or hides the activate and cancel buttons when buff was purchased or the purchase was canceled
    private void ShowOrHideBuffActivateOrCancelButtons(bool state)
    {
        ActivateOrCancelBuffButtons.SetActive(state);
    }


    //Checks if the current player has enought action points to purchase a buff and changes the purchase button to interactable or not interactable
    private void checkIfBuffPurchaseIsPossible()
    {
        if (actionsCounter.buffActivationPossible() && !trackableHandler.buffPurchased())
        {
            BuffPurchaseButton.interactable = true;
        }
        else
        {
            BuffPurchaseButton.interactable = false;
        }
    }


    //Shows or hides the purches button of a buff if buff is shown or not shown
    private void showOrHideBuffPurchaseButton()
    {
        if (trackableHandler.isBuffImageOnScreen())
        {
            ActivateBuffButtonOnScreen.SetActive(true);
        }
        else
        {
            ActivateBuffButtonOnScreen.SetActive(false);
        }

    }


    //Updates the remaining action points of the current player
    private void updateActionCountOnScreen()
    {
        RemainingActionCountOnScreen.text = "" + actionsCounter.getCountOfRemainingActions()+"/17";
    }

    /*
    private void updateGoldCountOnScreen()
    {
        //PlayerGold.text = ""+
    }

    private void updateSelectedUnitLife()
    {
        SelectedUnitLife.text = ""+ unit.TotalHitPoints;
    }

    private void showUITopLeft()
    {
        UITopLeft.SetActive(true);
    }
    */

    //Shows or hides the canvas and a purchased buff 
    private void showOrHideCanvas()
    {
        if (trackableHandler.trackableOfCanvas())
        {
            Canvas.SetActive(true);
            trackableHandler.showOrHidePurchasedBuff(true);
        }
        else
        {
            Canvas.SetActive(false);
            trackableHandler.showOrHidePurchasedBuff(false);
        }
    }


    //Closes the application if user clicks the "Beenden" button
    public void quitAllpication()
    {
        Application.Quit();
    }


    //Finises the turn of the current player and restarts the action count
    public void finishTurn()
    {
        turnCounter++;
        RoundCounter.text = "Runde" + "\n" + turnCounter;
        print("Turn: "+turnCounter);
        actionsCounter.restartAvailableActionPoints();
        CellGrid.EndTurn();
    }

  /*  public void endTurnMethod()
    {
        OnTurnEnded();
    }
    */
    //Event is called when the player end his/her turn, a end turn message is shown on the screen
    private void OnTurnEnded(object sender, EventArgs e)
    {
        if((sender as CellGrid).CurrentPlayer is HumanPlayer)
        {
            ChangeTurnScreen.SetActive(true);

            Invoke("ShowOrHideNextTurnMessege", 1);
        }
    }


    //Hides the turn switched message
    private void ShowOrHideNextTurnMessege()
    {
        ChangeTurnScreen.SetActive(false);
    }


    //Shows the end screen if the game is finished and displayes the winner of the game
    private void OnGameEnded(object sender, EventArgs e)
    {
        Canvas.SetActive(true);
        string victoriousPlayer;

        if((sender as CellGrid).CurrentPlayerNumber == 0)
        {
            victoriousPlayer = "Spieler 1";
        }
        else
        {
            victoriousPlayer = "Spieler 2";
        }
        EndScreenText.text = victoriousPlayer + " hat das Spiel gewonnen!!!";
        EndScreen.SetActive(true);
    }


    //Shows the costs of a action on the screen (called by ActionCount class)
    public void showCostsOnScreen(int cost)
    {
        DisplayCostForActionOnScreen.text = "-" + cost;
        DisplayCostForActionOnScreen.gameObject.SetActive(true);
        Invoke("hideCostDisplayOnScreen", 0.5f);
    }


    //After costs of a action were displayed, the costs message will be hidden
    private void hideCostDisplayOnScreen()
    {
        DisplayCostForActionOnScreen.gameObject.SetActive(false);
    }


    //Shows a attack not possible message if there are not enought action points to execute a attack (called by Unit class)
    public void showAttackNotPossibleMessage()
    {
        NotEnoughtActionPointsScreen.SetActive(true);
        Invoke("hideActionPointsMessage", 0.7f);
    }


    //Hides the attack is not possible message
    private void hideActionPointsMessage()
    {
        NotEnoughtActionPointsScreen.SetActive(false);
    }


    //Shows a message on screen that a buff was activated
    public void showBuffActivatedMessage()
    {
        BuffScreen.SetActive(true);
        Invoke("hideBuffScreenInformation", 1);

    }


    //Hides the buff was activated message
    private void hideBuffScreenInformation()
    {
        BuffScreen.SetActive(false);
    }
}
