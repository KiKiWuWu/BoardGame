using System.Collections.Generic;
using UnityEngine;

public class BuffExecuter : MonoBehaviour
{
    private List<Unit> listWithUnitsToBuff;
    private BuffSpawner buffSpawner = new BuffSpawner();
    private string currentBuffOnScreen;

    private AllUnitsController unitController;
    private CharacterAnimationController animationController;


    void Start()
    {
        unitController = gameObject.GetComponent<AllUnitsController>();
        animationController = gameObject.GetComponent<CharacterAnimationController>();

        listWithUnitsToBuff = new List<Unit>();
    }


    //Executes the current activated (purchased) buff on units within a list and hides the highlighter on screen of a unit
    public void executeBuffOnSelectedUnits()
    {
        if (currentBuffOnScreen == "attack")
        {
            AttackBuff attackBuff = new AttackBuff(1, 3);
            for (int i = 0; i < listWithUnitsToBuff.Count; i++)
            {
                buffSpawner.SpawnBuff(attackBuff, listWithUnitsToBuff[i]);
                hideHighlightFieldOfCharacter();
            }
        }

        if (currentBuffOnScreen == "defence")
        {
            DefenceBuff defenceBuff = new DefenceBuff(1, 2);
            for (int i = 0; i < listWithUnitsToBuff.Count; i++)
            {
                buffSpawner.SpawnBuff(defenceBuff, listWithUnitsToBuff[i]);
                hideHighlightFieldOfCharacter();
            }
        }

        if (currentBuffOnScreen == "heal")
        {
            HealingBuff healBuff = new HealingBuff(0, 5);
            for (int i = 0; i < listWithUnitsToBuff.Count; i++)
            {
                buffSpawner.SpawnBuff(healBuff, listWithUnitsToBuff[i]);
                hideHighlightFieldOfCharacter();
            }
        }
        clearListOfUnits();
    }


    //Clears the list of units after a buff was activated or canceled
    private void clearListOfUnits()
    {
        if(listWithUnitsToBuff.Count > 0)
        {
            listWithUnitsToBuff.Clear();
        }
    }


    //Hides the highlighters of the characters which are in the buff area
    private void hideHighlightFieldOfCharacter()
    {
        if (listWithUnitsToBuff.Count > 0)
        {
            for (int i = 0; i < listWithUnitsToBuff.Count; i++)
            {
                listWithUnitsToBuff[i].transform.FindChild("BuffHighlighter").gameObject.SetActive(false);
            }
        }
    }


    //Saves the name of the current activated (purchased) buff (sender is the TrackableImageOnScreenHandler.cs)
    public void currentActivatedBuff(string buffName)
    {
        currentBuffOnScreen = buffName;
    }


    //Return true of false if units are in the buff area (called by the GUIControllerHexa.cs)
    public bool areUnitsInBuffArea()
    {
        if (listWithUnitsToBuff.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //Updates the list of Units (send by the BuffCollider.cs)
    public void updateUnitList(List<Unit> unitsList)
    {
        listWithUnitsToBuff = unitsList;
    }


    //Returns the name of the current buff
    public string nameOfTheCurrentBuff()
    {
        return currentBuffOnScreen;
    }


    //Hides the highlighters of the characters which where in the buff area after the buff activation was canceled (called in TrackableImageOnScreenHandler.cs)
    public void hideHighlighterWhenBuffActivationIsCanceled()
    {
        hideHighlightFieldOfCharacter();
        clearListOfUnits();
    }


    //Unit takes the defence position after the player clicked the defence button
    public void unitTakesDefencePosition()
    {
        animationController.showCharacterTakingDefencePositionAnimation(unitController.currentlySelectedAlliedUnit());
        DefencePositionUnitBuff defencePosition = new DefencePositionUnitBuff(1, 1);
        buffSpawner.SpawnBuff(defencePosition, unitController.currentlySelectedAlliedUnit());
        unitController.currentlySelectedAlliedUnit().unitInDefencePosition = true;
        unitController.setAlliedUnitToFinishedState();
    }
}