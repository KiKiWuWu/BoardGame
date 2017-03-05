using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpecialAttackController : MonoBehaviour {

    private bool wasSpecialAttackSelectedByUser = false;
    private bool isSpecialAttackPossible = false;
    private bool specialAttackForCurrentChar = false;

    private ActionCount actionCount;


    void Start()
    {
        actionCount = gameObject.GetComponent<ActionCount>();
    }


    void Update()
    {
        isSpecialAttackPossible = actionCount.checkRemainingPointsForSpecialAttack();
    }


    //Special attack button is interactable if user didn´t select the button, special attack is possible (by remaining points) and current characters special attack is puchased (called by GUIControllerHexa class)
    public bool specialAttackButtonInteractable()
    {
        if(!wasSpecialAttackSelectedByUser && isSpecialAttackPossible && specialAttackForCurrentChar)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }


    //Returns true if the user selected the speacial attack button otherwise returns false (called by ActionCount class)
    public bool specialAttackActivatedByUser()
    {
        return wasSpecialAttackSelectedByUser;
    }

    

    //specialAttackForCurrentChar is true if the special attack is purchased on the selected character otherwise the variable is false (set by SampleUnit class)
    public void isSpecialAttackPossibleForCharacter(bool state)
    {
        specialAttackForCurrentChar = state;
    }


    //(called by SampleUnit and GUIControllerHexa class)
    public void setSpecialAttackButtonToDefault()
    {
        wasSpecialAttackSelectedByUser = false;
        isSpecialAttackPossible = false;
        specialAttackForCurrentChar = false;
}


    
    //
    public void activateSpecialAttack()
    {
        wasSpecialAttackSelectedByUser = true;
    }
}
