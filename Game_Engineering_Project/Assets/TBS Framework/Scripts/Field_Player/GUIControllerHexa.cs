using System;
using System.Collections;
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
    public Button ActivateSpecialAttackButton;

    public Text EndScreenText;
    public Text RemainingActionCountOnScreen;
    public Text ExecutedBuffTextOnScreen;
    public Text PlayerGold;
    public Text DisplayCostForActionOnScreen;
    public Text HPPlayer;
    public Text HPEnemy;
    public Text RoundCounter;

    public Slider HPSliderPlayer;
    public Slider HPSliderEnemy;

    public Image ImagePlayer;
    public Image ImageEnemy;

    private int turnCounter;
    private int currentActivePlayer;
    private Unit selectedEnemyUnit;


    private ActionCount actionsCounter;
    private TrackableImageOnScreenHandler trackableHandler;
    private BuffExecuter buffExecuter;
    private CharacterSpecialAttackController specialAttackController;



    //This function is called on start, initiates classes and EventHandler
    void Start()
    {
        actionsCounter = gameObject.GetComponent<ActionCount>();
        trackableHandler = gameObject.GetComponent<TrackableImageOnScreenHandler>();
        buffExecuter = gameObject.GetComponent<BuffExecuter>();
        specialAttackController = gameObject.GetComponent<CharacterSpecialAttackController>();

        turnCounter = 1;
        currentActivePlayer = CellGrid.CurrentPlayerNumber;
        CellGrid.TurnEnded += OnTurnEnded;
        CellGrid.GameEnded += OnGameEnded;
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
        checkInteractabilityOfSpecialAttackButton();
    }


    //Checks if the special attack button should be set to interactable or not interactable
    private void checkInteractabilityOfSpecialAttackButton()
    {
        if (specialAttackController.specialAttackButtonInteractable())
        {
            ActivateSpecialAttackButton.interactable = true;
        }
        else
        {
            ActivateSpecialAttackButton.interactable = false;
        }
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
        RemainingActionCountOnScreen.text = "" + actionsCounter.getCountOfRemainingActions() + " / " + actionsCounter.totalNumberOfActionPoints();
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

        currentActivePlayer = CellGrid.CurrentPlayerNumber;
    }


    //Event is called when the player end his/her turn, a end turn message is shown on the screen
    private void OnTurnEnded(object sender, EventArgs e)
    {
        if((sender as CellGrid).CurrentPlayer is HumanPlayer)
        {
            UITopLeft.SetActive(false);
            UITopRight.SetActive(false);
            ChangeTurnScreen.SetActive(true);

            specialAttackController.setSpecialAttackButtonToDefault();

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
        setBuffMessageOnScreen();
        BuffScreen.SetActive(true);
        Invoke("hideBuffScreenInformation", 1);
    }


    //Hides the buff was activated message
    private void hideBuffScreenInformation()
    {
        BuffScreen.SetActive(false);
    }


    //Gets the name of the activated buff and changes the text on screen
    private void setBuffMessageOnScreen()
    {
        string activatedBuffMessage = "";

        if (buffExecuter.nameOfTheCurrentBuff() == "attack")
        {
            activatedBuffMessage = "Die Angriffskraft wurde erhöht!";
        }

        if (buffExecuter.nameOfTheCurrentBuff() == "defence")
        {
            activatedBuffMessage = "Die Verteidigung wurde erhöht!";
        }

        if (buffExecuter.nameOfTheCurrentBuff() == "heal")
        {
            activatedBuffMessage = "Eine Heilung wurde aktiviert!";
        }

        ExecutedBuffTextOnScreen.text = activatedBuffMessage;
    }


    //
    public void showDamageInHPBar(int tempHitpoints, int HitPoints, int TotalHitPoints)
    {
        StartCoroutine(dmgDelay(tempHitpoints, HitPoints, TotalHitPoints));
    }


    //
    private IEnumerator dmgDelay(int previousHitpoints, int currentHitpoints, int maxHitpoints)
    {
        float HPproc = ((float)(previousHitpoints) / maxHitpoints) * 100;
        HPSliderEnemy.value = (float)HPproc;
        HPEnemy.text = "" + previousHitpoints + "/" + maxHitpoints + " HP";

        int HPLost = previousHitpoints - currentHitpoints;
        for (int i = 0; i < HPLost; i++)
        {
            //print("Time:" + Time.time);
            yield return new WaitForSeconds(0.03f);
            float HPproc1 = ((float)(previousHitpoints - 1) / maxHitpoints) * 100;
            HPSliderEnemy.value = (float)HPproc1;
            HPEnemy.text = "" + (previousHitpoints - 1) + "/" + maxHitpoints + " HP";


            //print("Time:" + Time.time);
            previousHitpoints--;
        }
        if(currentHitpoints <= 0)
        {
            UITopRight.SetActive(false);
        }
    }


    //Shows the health bar of a foe or enemy on screen with given parameters (called by Unit class)
    public void showHPBarOfSelectedUnit(string unitFraction, Unit selectedUnit)
    {
        float HPInProcent = ((float)selectedUnit.HitPoints / selectedUnit.TotalHitPoints) * 100;
        string healthPointsText = "" + selectedUnit.HitPoints + "/" + selectedUnit.TotalHitPoints + " HP";

        if (unitFraction == "foe")
        {
            UITopLeft.SetActive(true);
            HPSliderPlayer.value = HPInProcent;
            HPPlayer.text = healthPointsText;
        }

        if (unitFraction == "enemy")
        {
            UITopRight.SetActive(true);
            HPSliderEnemy.value = HPInProcent;
            HPEnemy.text = healthPointsText;
        }
    }


    //Hides the HP bar of the current selected character or the selected enemy unit (called by SampleUnit class)
    public void hideHPBarOnScreen(string unitFraction)
    {
        if(unitFraction == "foe")
        {
            UITopLeft.SetActive(false);
        }

        if(unitFraction == "enemy")
        {
            UITopRight.SetActive(false);
        }
    }


    //Return number of the player who is currently active (called by Unit class)
    public int currentlyActivePlayer()
    {
        return currentActivePlayer;
    }


    //Returns the enemy unit which HP bar is currently displayed on screen
    public Unit currentSelectedEnemyUnit()
    {
        return selectedEnemyUnit;
    }


    //Stores the selected enemy Unit
    public void storeSelectedEnemyUnit(Unit selectedEnemy)
    {
        selectedEnemyUnit = selectedEnemy;
    }
}
