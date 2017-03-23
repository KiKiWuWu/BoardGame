using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GUIControllerHexa : MonoBehaviour
{
    public CellGrid CellGrid;

    public GameObject Canvas;
    public GameObject UITopLeft;
    public GameObject UITopRight;
    public GameObject ActivateOrCancelBuffButtons;
    public GameObject SpecialAttackButtonsArea;
    public GameObject CastleButtonsArea;

    public Button BuffPurchaseButton;
    public Button ActivateBuffButton;
    public Button ActivateSpecialAttackButton;
    public Button PurchaseSpecialAttackButton;
    public Button NeutralizeCastleButton;
    public Button ConquerCastleButton;

    public Text RemainingActionCountOnScreen;
    public Text DisplayCostForActionOnScreen;
    public Text HPPlayer;
    public Text HPEnemy;
    public Text PlayerUnitNameText;
    public Text FoeUnitNameText;
    public Text RoundCounter;
    public Text GoldCountOnScreen;

    public Slider HPSliderPlayer;
    public Slider HPSliderEnemy;

    public Image ImagePlayer;
    public Image ImageEnemy;

    private MessagesOnScreenController messageOnScreenController;
    private ActionCount actionsCounter;
    private TrackableImageOnScreenHandler trackableHandler;
    private BuffExecuter buffExecuter;
    private CharacterSpecialAttackController specialAttackController;
    private GoldController goldController;
    private TurnCounter turnCounter;
    private AllUnitsController unitController;
    private FieldDestroyerController fieldDestroyController;
    private CastleController castleController;



    //This function is called on start, initiates classes and EventHandler
    void Start()
    {
        messageOnScreenController = gameObject.GetComponent<MessagesOnScreenController>();
        actionsCounter = gameObject.GetComponent<ActionCount>();
        trackableHandler = gameObject.GetComponent<TrackableImageOnScreenHandler>();
        buffExecuter = gameObject.GetComponent<BuffExecuter>();
        specialAttackController = gameObject.GetComponent<CharacterSpecialAttackController>();
        goldController = gameObject.GetComponent<GoldController>();
        turnCounter = gameObject.GetComponent<TurnCounter>();
        unitController = gameObject.GetComponent<AllUnitsController>();
        fieldDestroyController = gameObject.GetComponent<FieldDestroyerController>();
        castleController = gameObject.GetComponent<CastleController>();
        
        CellGrid.GameEnded += GetOnGameEnded();

        RoundCounter.text = "Runde" + "\n" + turnCounter.currentTurn();
    }

    
    //Handles the Event game ended when it is called
    private EventHandler GetOnGameEnded()
    {
        return OnGameEnded;
    }
    

    //Update function is called every frame
    void Update()
    {
        showOrHideCanvas();
        updateActionCountOnScreen();
        checkInteractabilityOfBuffButton();
        showOrHideBuffActivateOrCancelButtons();
        checkIfUnitsAreInBuffArea();
        updateGoldCountOnScreen();
        checkInteractabilityOfSpecialAttackButton();
        checkIfCharacterStandsOnACastle();
    }


    //Checks if a character is standing on a castle and shows buttons depending on the occupation state of the castle
    private void checkIfCharacterStandsOnACastle()
    {
        if (unitController.currentlySelectedAlliedUnit() != null && unitController.currentlySelectedAlliedUnit().standOnCastle != null)
        {
            if (unitController.currentlySelectedAlliedUnit().standOnCastle.occupiedStateOfCastle(unitController.activePlayer()) == "neutralize")
            {
                ConquerCastleButton.gameObject.SetActive(false);
                NeutralizeCastleButton.gameObject.SetActive(true);
            }

            if (unitController.currentlySelectedAlliedUnit().standOnCastle.occupiedStateOfCastle(unitController.activePlayer()) == "conquer")
            {
                NeutralizeCastleButton.gameObject.SetActive(false);
                ConquerCastleButton.gameObject.SetActive(true);
                ConquerCastleButton.interactable = true;
            }

            if (unitController.currentlySelectedAlliedUnit().standOnCastle.occupiedStateOfCastle(unitController.activePlayer()) == "conquered")
            {
                NeutralizeCastleButton.gameObject.SetActive(false);
                ConquerCastleButton.gameObject.SetActive(true);
                ConquerCastleButton.interactable = false;
            }
            CastleButtonsArea.SetActive(true);
        }
        else
        {
            CastleButtonsArea.SetActive(false);
        }
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


    //Checks the interactability of the buff purchase button
    private void checkInteractabilityOfBuffButton()
    {
        if (trackableHandler.buffTrackableImagePresent() && !trackableHandler.buffPurchased())
        {
            BuffPurchaseButton.interactable = true;
        }
        else
        {
            BuffPurchaseButton.interactable = false;
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

        UITopLeft.SetActive(false);
        UITopRight.SetActive(false);

        unitController.selectedAlliedUnitByPlayer(null);
        unitController.selectedEnemyUnitByPlayer(null);
        
        CellGrid.EndTurn();

        statusOfCastlesOnTheField();
        unitController.changeCurrentActivePlayer();
        goldController.addTurnGoldToPlayersGoldCount();


        messageOnScreenController.showNextPlayerScreen();
    }


    //Checks if gold (depending on the number of occupied castles) should be added to the gold count of the current player or the castles on the field should be destroyed
    private void statusOfCastlesOnTheField()
    {
        if(turnCounter.currentTurn() < fieldDestroyController.startFieldDestructionTurn())
        {
            goldController.addGoldForEveryOccupiedCastleToPlayersGoldCount();
        }
        else if(turnCounter.currentTurn() == fieldDestroyController.startFieldDestructionTurn())
        {
            castleController.destroyAllCastlesOnTheField();
        }
    }


    //Shows the end screen if the game is finished and displayes the winner of the game or a draw message
    private void OnGameEnded(object sender, EventArgs e)
    {
        messageOnScreenController.showEndScreen((sender as CellGrid).getPlayerWithLastUnitOnTheField());
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
        messageOnScreenController.showNotEnoughtAPForAttackScreen();
    }


    //Checks if the buff on screen can be executed or not (if not a message with the reason for that will be displayed on screen)
    public void executeBuffButtonPressedByPlayer()
    {
        if (!goldController.isPurchaseOfABuffPossible() && !actionsCounter.buffActivationPossible())
        {
            messageOnScreenController.buffActivationIsNotPossibleScreen("goldAndAP");
        }
        else if (!goldController.isPurchaseOfABuffPossible())
        {
            messageOnScreenController.buffActivationIsNotPossibleScreen("gold");
        }
        else if (!actionsCounter.buffActivationPossible())
        {
            messageOnScreenController.buffActivationIsNotPossibleScreen("AP");
        }
        else
        {
            buffExecuter.executeBuffOnSelectedUnits();
            showBuffActivatedMessage();
            actionsCounter.subtractCostOfActionFromCurrentActionCount("buff");
            goldController.reduceGoldAfterAPurchase("buff");
        }
        trackableHandler.destroyBuffIfOnScreen();
    }


    //Shows a message on screen that a buff was activated
    private void showBuffActivatedMessage()
    {
        messageOnScreenController.showBuffMessageOnScreen();
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
                PlayerUnitNameText.text = unitController.currentlySelectedAlliedUnit().name;
                HPSliderPlayer.value = HPInProcent;
                HPPlayer.text = healthPointsText;
            }

            if (unitFraction == "enemy")
            {
                UITopRight.SetActive(true);
                FoeUnitNameText.text = unitController.currentlySelectedEnemyUnit().name;
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
        if(unitFraction == "friend")
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