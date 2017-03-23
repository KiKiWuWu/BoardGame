using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagesOnScreenController : MonoBehaviour
{
    public GameObject ChangeTurnScreen;
    public GameObject EndScreen;
    public GameObject BuffScreen;
    public GameObject NotEnoughtActionPointsForAttackScreen;
    public GameObject NotEnoughtActionPointsForSpecialAttackScreen;
    public GameObject NotEnoughtGoldOrActionPointsToExecuteBuffScreen;
    public GameObject CastleStatusInformationScreen;
    public GameObject FieldsWillBeDestroyedInformationScreen;


    private BuffExecuter buffExecuter;
    private ActionCount actionCounter;
    private GoldController goldController;

    // Use this for initialization
    void Start () {
        buffExecuter = gameObject.GetComponent<BuffExecuter>();
        actionCounter = gameObject.GetComponent<ActionCount>();
        goldController = gameObject.GetComponent<GoldController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    //Shows a switch player message on the screen
    public void showNextPlayerScreen()
    {
        ChangeTurnScreen.SetActive(true);
    }


    //Sets the end screen message and displays the end screen
    public void showEndScreen(string endGameMessage)
    {
        string messageToDisplayOnScreen = "";

        if (endGameMessage == "0")
        {
            messageToDisplayOnScreen = "Spieler 1 hat das Spiel gewonnen!!!";
        }
        else if (endGameMessage == "1")
        {
            messageToDisplayOnScreen = "Spieler 2 hat das Spiel gewonnen!!!";
        }
        else if (endGameMessage == "-1")
        {
            messageToDisplayOnScreen = "Unentschieden!!!";
        }
        EndScreen.transform.FindChild("EndScreenMessege").GetComponent<Text>().text = messageToDisplayOnScreen;
        EndScreen.SetActive(true);
    }


    //Sets a message depending on which buff was activated, shows the message on screen for a set time and then hides the message
    public void showBuffMessageOnScreen()
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


    //Gets the name of the activated buff and changes the text on screen
    private void setBuffMessageOnScreen()
    {
        Text messageOnScreen = BuffScreen.transform.FindChild("TextInformationOnBuff").GetComponent<Text>();
        string activatedBuffMessage = "";

        if (buffExecuter.nameOfTheCurrentBuff() == "attack")
        {
            activatedBuffMessage = "Die Angriffskraft wurde erhöht!";
            messageOnScreen.color = Color.red;
        }

        if (buffExecuter.nameOfTheCurrentBuff() == "defence")
        {
            activatedBuffMessage = "Die Verteidigung wurde erhöht!";
            messageOnScreen.color = Color.cyan;
        }

        if (buffExecuter.nameOfTheCurrentBuff() == "heal")
        {
            activatedBuffMessage = "Eine Heilung wurde aktiviert!";
            messageOnScreen.color = Color.green;
        }
        messageOnScreen.text = activatedBuffMessage;
    }


    //Shows a message on screen that there are not enough action points to attack an enemy
    public void showNotEnoughtAPForAttackScreen()
    {
        NotEnoughtActionPointsForAttackScreen.SetActive(true);
        Invoke("hideActionPointsMessage", 0.7f);
    }


    //Hides the attack is not possible message
    private void hideActionPointsMessage()
    {
        NotEnoughtActionPointsForAttackScreen.SetActive(false);
    }


    //Shows the special attack is not possible message on screen (called by CharacterSpecialAttackController.cs)
    public void showSpecialAttackNotPossibleScreen()
    {
        NotEnoughtActionPointsForSpecialAttackScreen.SetActive(true);
    }


    //Shows a message that the activation of a buff is not possible (message is shown when not enought gold, not enought action points or both)
    public void buffActivationIsNotPossibleScreen(string reason)
    {
        Text messageOnScreen = NotEnoughtGoldOrActionPointsToExecuteBuffScreen.transform.FindChild("InfoTextMessage").GetComponent<Text>();
        string standartMessage = "Um einen Buff zu aktivieren benötigen Sie " + goldController.numberOfGoldToActivateBuff() + " Gold und " + actionCounter.numberOfActionPointsToActivateBuff() + " Aktionspunkte!";
        string messageToDisplay = "";

        if (reason == "gold")
        {
            messageToDisplay = "Sie haben nicht genug Gold! \n \n";
            messageToDisplay += standartMessage;
        }
        else if(reason == "AP")
        {
            messageToDisplay = "Sie haben nicht genug Aktionspunkte! \n \n";
            messageToDisplay += standartMessage;
        }
        else if(reason == "goldAndAP")
        {
            messageToDisplay = "Sie haben nicht genug Gold und Aktionspunkte! \n \n";
            messageToDisplay += standartMessage;
        }
        messageOnScreen.text = messageToDisplay;
        NotEnoughtGoldOrActionPointsToExecuteBuffScreen.SetActive(true);
    }


    //Shows a message on screen depending on the state of castle an remaining action points
    public void showMessageOnScreenAboutCastleState(string command)
    {
        Text CastleInformationText = CastleStatusInformationScreen.transform.FindChild("CastleInformationText").GetComponent<Text>();

        if (command == "castleNeutralized")
        {
            CastleInformationText.text = "Die Burg wurde erfolgreich neutralisiert!";
        }

        if (command == "castleOccupied")
        {
            CastleInformationText.text = "Die Burg wurde erfolgreich eingenommen!";
        }

        if (command == "notEnoughtPoints")
        {
            CastleInformationText.text = "Sie haben nicht genug Aktionspunkte! \n \n Sie benötigen " + actionCounter.getPointsForOccupyingCastle() + " Aktionspunkte";
        }

        CastleStatusInformationScreen.SetActive(true);
    }


    //Displays the number of turn when the field starts to crumble or a message that the field is currently crumbling
    public void showFieldCrumbleMessageScreen(int turnNumber)
    {
        Text messageOnScreen = FieldsWillBeDestroyedInformationScreen.transform.FindChild("FieldCrumbleText").GetComponent<Text>();

        if (turnNumber == 2)
        {
            messageOnScreen.text = "Noch zwei Runden.";
        }
        else if (turnNumber == 1)
        {
            messageOnScreen.text = "Noch eine Runde.";
        }
        else if (turnNumber == 0)
        {
            messageOnScreen.text = "Felder werden zerstört.";
        }
        FieldsWillBeDestroyedInformationScreen.SetActive(true);
        Invoke("hideFieldCrumbleMessageScreen", 2.5f);
    }


    //Hides the field crumble message on screen
    private void hideFieldCrumbleMessageScreen()
    {
        FieldsWillBeDestroyedInformationScreen.SetActive(false);
    }
}