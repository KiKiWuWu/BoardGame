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
    public GameObject SpecialAttackNotPossibleScreen;
    public GameObject NotEnoughtGoldToExecuteBuffScreen;
    public GameObject ActivateOrCancelBuffButtons;
    public GameObject SpecialAttackButtonsArea;

    public Button BuffPurchaseButton;
    public Button ActivateBuffButton;
    public Button ActivateSpecialAttackButton;
    public Button PurchaseSpecialAttackButton;

    public Text EndScreenText;
    public Text RemainingActionCountOnScreen;
    public Text ExecutedBuffTextOnScreen;
    public Text DisplayCostForActionOnScreen;
    public Text HPPlayer;
    public Text HPEnemy;
    public Text RoundCounter;
    public Text GoldCountOnScreen;

    public Slider HPSliderPlayer;
    public Slider HPSliderEnemy;

    public Image ImagePlayer;
    public Image ImageEnemy;


    private ActionCount actionsCounter;
    private TrackableImageOnScreenHandler trackableHandler;
    private BuffExecuter buffExecuter;
    private CharacterSpecialAttackController specialAttackController;
    private GoldController goldController;
    private TurnCounter turnCounter;
    private AllUnitsController unitController;



    //This function is called on start, initiates classes and EventHandler
    void Start()
    {
        actionsCounter = gameObject.GetComponent<ActionCount>();
        trackableHandler = gameObject.GetComponent<TrackableImageOnScreenHandler>();
        buffExecuter = gameObject.GetComponent<BuffExecuter>();
        specialAttackController = gameObject.GetComponent<CharacterSpecialAttackController>();
        goldController = gameObject.GetComponent<GoldController>();
        turnCounter = gameObject.GetComponent<TurnCounter>();
        unitController = gameObject.GetComponent<AllUnitsController>();

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
        updateGoldCountOnScreen();

        checkInteractabilityOfSpecialAttackButton();
    }


    //Checks if the special attack button should be set to interactable or not interactable
    private void checkInteractabilityOfSpecialAttackButton()
    {        
        if(unitController.currentlySelectedAlliedUnit() != null)
        {
            if (unitController.currentlySelectedAlliedUnit().specialAttackPurchased)
            {
                checkInteractabilityOfActivateSpecialAttackButton();
                ActivateSpecialAttackButton.gameObject.SetActive(true);
                PurchaseSpecialAttackButton.gameObject.SetActive(false);
            }
            else
            {
                checkInteractabilityOfPurchaseSpecialAttackButton();
                ActivateSpecialAttackButton.gameObject.SetActive(false);
                PurchaseSpecialAttackButton.gameObject.SetActive(true);
            }
        }
    }


    //Checks if it is possible to purchase the special attack for the currently selected unit
    private void checkInteractabilityOfPurchaseSpecialAttackButton()
    {
        if (goldController.isSpecialAttackPurchasePossible())
        {
            PurchaseSpecialAttackButton.interactable = true;
        }
        else
        {
            PurchaseSpecialAttackButton.interactable = false;
        }
    }


    //Checks if it is possible to activate the special attack of the currently selected unit
    private void checkInteractabilityOfActivateSpecialAttackButton()
    {
        if (specialAttackController.specialAttackActivatedByUser())
        {
            ActivateSpecialAttackButton.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);
        }
        else
        {
            ActivateSpecialAttackButton.GetComponent<Image>().color = new Color(1, 1, 1);
        }
    }


    //Displays the amount of gold that the current player posseses
    private void updateGoldCountOnScreen()
    {
        GoldCountOnScreen.text = "" + goldController.getGoldCountOfActivePlayer();
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
        turnCounter.changeTurn();
        RoundCounter.text = "Runde" + "\n" + turnCounter.currentTurn();
        actionsCounter.restartAvailableActionPoints();
        CellGrid.EndTurn();

        unitController.selectedEnemyUnitByPlayer(null);

        unitController.changeCurrentActivePlayer();
        goldController.addGoldToPlayersGoldCount();
    }


    //Event is called when the player end his/her turn, a end turn message is shown on the screen
    private void OnTurnEnded(object sender, EventArgs e)
    {
        UITopLeft.SetActive(false);
        UITopRight.SetActive(false);
        ChangeTurnScreen.SetActive(true);
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


    //Checks if the buff on screen can be executed or not
    public void executeBuffButtonPressedByPlayer()
    {
        if (!goldController.isPurchaseOfABuffPossible())
        {
            NotEnoughtGoldToExecuteBuffScreen.SetActive(true);
        }
        else
        {
            buffExecuter.executeBuffOnSelectedUnits();
            trackableHandler.destroyBuffIfOnScreen();
            showBuffActivatedMessage();
            actionsCounter.subtractCostOfActionFromCurrentActionCount("buff");
            goldController.reduceGoldAfterAPurchase("buff");
        }
    }


    //Shows a message on screen that a buff was activated
    private void showBuffActivatedMessage()
    {
        setBuffMessageOnScreen();
        BuffScreen.SetActive(true);
        Invoke("hideBuffScreenInformation", 1.5f);
    }


    //Hides the buff was activated message
    private void hideBuffScreenInformation()
    {
        BuffScreen.SetActive(false);
    }


    //Shows the special attack is not possible message on screen (called by CharacterSpecialAttackController class)
    public void showInfoScreenThatSpecialAttackIsNotPossible()
    {
        SpecialAttackNotPossibleScreen.SetActive(true);
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


    //Start a coroutine that reduces the health points of the attacked enemy
    public void showDamageInHPBar(int tempHitpoints, int HitPoints, int TotalHitPoints)
    {
        StartCoroutine(dmgDelay(tempHitpoints, HitPoints, TotalHitPoints));
    }


    //Reduces the health points of the attacked enemy and hides the HP bar if the health of the attacked unit reaches zero
    private IEnumerator dmgDelay(int previousHitpoints, int currentHitpoints, int maxHitpoints)
    {        
        int HPLost = previousHitpoints - currentHitpoints;
        for (int i = 0; i < HPLost; i++)
        {
            float HPproc1 = ((float)(previousHitpoints - 1) / maxHitpoints) * 100;
            HPSliderEnemy.value = HPproc1;
            HPEnemy.text = "" + (previousHitpoints - 1) + "/" + maxHitpoints + " HP";

            previousHitpoints--;

            yield return new WaitForSeconds(0.1f);
        }
        if (currentHitpoints == 0)
        {
            UITopRight.SetActive(false);
        }
    }


    //Shows the health bar of a foe or enemy on screen with given parameters (called by Unit and SampleUnit class)
    public void showHPBarOfSelectedUnit(string unitFraction, int hitPoints, int totalHitPoints)
    {
        if (unitFraction != "attackOnEnemy")
        {
            float HPInProcent = ((float)hitPoints / totalHitPoints) * 100;
            string healthPointsText = "" + hitPoints + "/" + totalHitPoints + " HP";

            if (unitFraction == "friend")
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
        else
        { 
           UITopRight.SetActive(true);
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


    //Hides the special attack button on screen
    public void hideSpecialAttackButtonArea()
    {
        SpecialAttackButtonsArea.SetActive(false);
    }


    //Shows the special attack button on screen
    public void showSpecialAttackButtonArea()
    {
        SpecialAttackButtonsArea.SetActive(true);
    }
}
