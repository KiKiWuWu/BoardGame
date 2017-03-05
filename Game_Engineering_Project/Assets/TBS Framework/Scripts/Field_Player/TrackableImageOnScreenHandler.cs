using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackableImageOnScreenHandler : MonoBehaviour
{
    public Transform SpawnPositionOfActivatedBuffs;
    public GameObject AttackBuff;
    public GameObject DefenceBuff;
    public GameObject HealingBuff;

    private GameObject currentBuffOnScreen;
    private GameObject createdBuff;

    //private bool canvasState = false;
    private int numberOfDisplayedImageTargets = 0;
    private BuffExecuter buffExecuter;


    //This function is called on start
    void Start()
    {
        buffExecuter = gameObject.GetComponent<BuffExecuter>();
    }


    //Gets the trackable(-s) which are currently displayed before the camera (sender is DefaultTrackableEventHandler class)
    public void trackableEnabled(string trackableName)
    {
        numberOfDisplayedImageTargets++;

        if (trackableName == "stones")
        {
            //canvasState = true;
        }

        if (trackableName == "AttackUpBuff")
        {
            currentBuffOnScreen = AttackBuff;
            buffExecuter.currentActivatedBuff("attack");
        }

        if (trackableName == "DefenceUpBuff")
        {
            currentBuffOnScreen = DefenceBuff;
            buffExecuter.currentActivatedBuff("defence");
        }

        if (trackableName == "HealthUpBuff")
        {
            currentBuffOnScreen = HealingBuff;
            buffExecuter.currentActivatedBuff("heal");
        }
    }


    //Gets the trackable(-s) which are NOT currently displayed before the camera (sender is DefaultTrackableEventHandler class)
    public void trackableDisabled(string trackableName)
    {
        if (numberOfDisplayedImageTargets > 0)
        {
            numberOfDisplayedImageTargets--;
        }
            
        if (trackableName == "stones")
        {
            //canvasState = false;
        }

        if (trackableName == "AttackUpBuff")
        {
            currentBuffOnScreen = null;
        }

        if (trackableName == "DefenceUpBuff")
        {
            currentBuffOnScreen = null;
        }

        if (trackableName == "HealthUpBuff")
        {
            currentBuffOnScreen = null;
        }
    }


    //Instatiates (clones) a buff in the middle of the play field, makes it visible and activates the touch functions on it (the user can move the buff on the field) 
    public void purchaseBuff()
    {
        createdBuff = Instantiate(currentBuffOnScreen, SpawnPositionOfActivatedBuffs.position, SpawnPositionOfActivatedBuffs.rotation);
        createdBuff.SetActive(true);
        createdBuff.GetComponent<Lean.Touch.LeanSelectable>().IsSelected = true;
    }


    //Destroys the purchased buff if it was activated or when the turn ends
    public void destroyBuffIfOnScreen()
    {
        if(createdBuff != null)
        {
            Destroy(createdBuff);
            buffExecuter.hideHighlighterWhenBuffActivationIsCanceled();
        }
    }


    //Shows or hides the purchased buff depending if the canvas is shown or not and if a buff is purchased or not (called by the GUIControllerHexa class)
    public void showOrHidePurchasedBuff(bool canvasIsShown)
    {
        if(createdBuff != null) { 
            if (canvasIsShown)
            {
                createdBuff.SetActive(true);
            }
            else
            {
                createdBuff.SetActive(false);
            }
        }
    }


    //Returns a true or false if a image of a buff target is currently displayed or not (called by GUIControllerHexa class)
    public bool isBuffImageOnScreen()
    {
        if (currentBuffOnScreen != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //Returns true or false if a buff was purchased (saved as activated) or not (called by the GUIControllerHexa class)
    public bool buffPurchased()
    {
        if (createdBuff != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //Returns true or false if a image target is currently shown or not (called by GUIControllerHexa class)
    public bool trackableOfCanvas()
    {
        if(numberOfDisplayedImageTargets > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        //return canvasState;
    }
}
