using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

    public GameObject TutorialScreens;
    public Button EndTurnButton;

    private TrackableImageOnScreenHandler imageHandler;
    private AllUnitsController unitController;
    private GoldController goldController;
    private TurnCounter turnCounter;

    private bool firstPartOfTutorialIsOver = false;
    private bool secondPartOfTutorialIsOver = false;
    private bool thirdPartOfTutorialIsOver = false;
    private bool fourthPartOfTutorialIsOver = false;
    private bool fifthPartOfTutorialIsOver = false;
    private bool sixthPartOfTutorialIsOver = false;

    private bool tutorialIsOver = false;

    private bool secondPartAlreadyShown = false;
    private bool thirdPartAlreadyShown = false;
    private bool fourthPartAlreadyShown = false;
    private bool fifthPartAlreadyShown = false;
    private bool sixthPartAlreadyShown = false;

    private int tutorialProgressCount = 1;


    // Use this for initialization
    void Start () {
        imageHandler = gameObject.GetComponent<TrackableImageOnScreenHandler>();
        unitController = gameObject.GetComponent<AllUnitsController>();
        goldController = gameObject.GetComponent<GoldController>();
        turnCounter = gameObject.GetComponent<TurnCounter>();

        addExtraGold();
	}


    //Adds extra Gold to the player one count
    private void addExtraGold()
    {
        for (int i = 0; i < 4; i++)
        {
            goldController.addPowerUpGoldForCurrentPlayer();
        }
    }


    // Update is called once per frame
    void Update () {
        if (!tutorialIsOver)
        {
            if (firstPartOfTutorialIsOver && !secondPartAlreadyShown)
            {
                checkIfPartTwoOfTutorialToShow();
            }

            if (secondPartOfTutorialIsOver && !thirdPartAlreadyShown)
            {
                checkIfPartThreeOfTutorialToShow();
            }

            if(thirdPartOfTutorialIsOver && !fourthPartAlreadyShown)
            {
                checkIfPartFourOfTutorialToShow();
            }

            if(fourthPartOfTutorialIsOver && !fifthPartAlreadyShown)
            {
                checkIfPartFiveOfTutorialToShow();
            }

            if(fifthPartOfTutorialIsOver && !sixthPartAlreadyShown)
            {
                checkIfPartSixOfTutorialToShow();
            }

            if(sixthPartOfTutorialIsOver && !tutorialIsOver)
            {
                checkIfFinalPartOfTutorialToShow();
            }
        }
	}


    //Checks if the conditions are right to shows the final part of the tutorial 
    private void checkIfFinalPartOfTutorialToShow()
    {
        if(turnCounter.currentTurn() == 11)
        {
            Invoke("showTutorialScreenForPlayer", 1f);
        }
    }


    //Checks if the conditions are right to shows the sixth part of the tutorial 
    private void checkIfPartSixOfTutorialToShow()
    {
        if(turnCounter.currentTurn() == 9)
        {
            Invoke("showTutorialScreenForPlayer", 1f);
        }
    }


    //Checks if the conditions are right to shows the fifth part of the tutorial 
    private void checkIfPartFiveOfTutorialToShow()
    {
        if(turnCounter.currentTurn() == 7)
        {
            Invoke("showTutorialScreenForPlayer", 1f);
        }
    }


    //Checks if the conditions are right to shows the fourth part of the tutorial 
    private void checkIfPartFourOfTutorialToShow()
    {
        if(turnCounter.currentTurn() == 3)
        {
            Invoke("showTutorialScreenForPlayer", 1f);
        }
    }


    //Checks if the conditions are right to shows the third part of the tutorial 
    private void checkIfPartThreeOfTutorialToShow()
    {
        if(unitController.currentlySelectedAlliedUnit() != null || unitController.currentlySelectedEnemyUnit() != null)
        {
            Invoke("showTutorialScreenForPlayer", 1f);
        }
    }


    //Checks if the conditions are right to shows the second part of the tutorial 
    private void checkIfPartTwoOfTutorialToShow()
    {
        if (imageHandler.trackableOfCanvas())
        {
            Invoke("showTutorialScreenForPlayer", 1f);
        }
    }


    //Shows the tutorial screen denpending on the progress of the game and the tutorial
    private void showTutorialScreenForPlayer()
    {
        if(tutorialProgressCount == 2)
        {
            TutorialScreens.transform.FindChild("TutorialScreen2").gameObject.SetActive(true);
            secondPartAlreadyShown = true;
        }
        else if (tutorialProgressCount == 3)
        {
            TutorialScreens.transform.FindChild("TutorialScreen7").gameObject.SetActive(true);
            thirdPartAlreadyShown = true;
            enableEndTurnButton();
        }
        else if (tutorialProgressCount == 4)
        {
            TutorialScreens.transform.FindChild("TutorialScreen13").gameObject.SetActive(true);
            fourthPartAlreadyShown = true;
        }
        else if (tutorialProgressCount == 5)
        {
            TutorialScreens.transform.FindChild("TutorialScreen16").gameObject.SetActive(true);
            fifthPartAlreadyShown = true;
        }
        else if (tutorialProgressCount == 6)
        {
            TutorialScreens.transform.FindChild("TutorialScreen18").gameObject.SetActive(true);
            sixthPartAlreadyShown = true;
        }
        else if (tutorialProgressCount == 7)
        {
            TutorialScreens.transform.FindChild("TutorialScreen20").gameObject.SetActive(true);
            tutorialIsOver = true;
        }
    }


    ////Is called when the player presses the conform button in the tutorial and depending on the count the the variable changes
    public void tutorialConfirmButtonClicked()
    {
        if(tutorialProgressCount == 1)
        {
            firstPartOfTutorialIsOver = true;
        }
        else if (tutorialProgressCount == 2)
        {
            secondPartOfTutorialIsOver = true;
        }
        else if (tutorialProgressCount == 3)
        {
            thirdPartOfTutorialIsOver = true;
        }
        else if (tutorialProgressCount == 4)
        {
            fourthPartOfTutorialIsOver = true;
        }
        else if (tutorialProgressCount == 5)
        {
            fifthPartOfTutorialIsOver = true;
        }
        else if (tutorialProgressCount == 6)
        {
            sixthPartOfTutorialIsOver = true;
        }
        else if(tutorialProgressCount == 7)
        {
            TutorialScreens.SetActive(false);
            tutorialIsOver = true;
        }
        tutorialProgressCount++;
        disableEndTurnButtonForShortTime();
    }


    //To prevent the player from clicking the end turn button by mistake the end turn button is disabled für a short amount of time 
    private void disableEndTurnButtonForShortTime()
    {
        if(tutorialProgressCount != 3)
        {
            EndTurnButton.interactable = false;
            Invoke("enableEndTurnButton", 2f);
        }
    }

    //Sets the end turn button to its normal state so the player can use it again
    private void enableEndTurnButton()
    {
        EndTurnButton.interactable = true;
    }
}
