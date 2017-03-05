using System.Collections.Generic;
using UnityEngine;

public class BuffExecuter : MonoBehaviour
{
    private List<Unit> listWithUnitsToBuff;
    private BuffSpawner buffSpawner = new BuffSpawner();
    private string currentBuffOnScreen;


    void Start()
    {
        listWithUnitsToBuff = new List<Unit>();
    }


    //Executes the current activated (purchased) buff on units within a list and hides the highlighter on screen of a unit
    public void executeBuffOnSelectedUnits()
    {
        if (currentBuffOnScreen == "attack")
        {
            AttackBuff attackBuff = new AttackBuff(0, 3);
            for (int i = 0; i < listWithUnitsToBuff.Count; i++)
            {
                buffSpawner.SpawnBuff(attackBuff, listWithUnitsToBuff[i]);
                hideHighlightFiledOfCharacter();
            }
        }

        if (currentBuffOnScreen == "defence")
        {
            DefenceBuff defenceBuff = new DefenceBuff(1, 2);
            for (int i = 0; i < listWithUnitsToBuff.Count; i++)
            {
                buffSpawner.SpawnBuff(defenceBuff, listWithUnitsToBuff[i]);
                hideHighlightFiledOfCharacter();
            }
        }

        if (currentBuffOnScreen == "heal")
        {
            HealingBuff healBuff = new HealingBuff(0, 5);
            for (int i = 0; i < listWithUnitsToBuff.Count; i++)
            {
                buffSpawner.SpawnBuff(healBuff, listWithUnitsToBuff[i]);
                hideHighlightFiledOfCharacter();
            }
        }

        listWithUnitsToBuff.Clear();
    }


    //Hides the highlighters of the characters which are in the buff area
    private void hideHighlightFiledOfCharacter()
    {
        if (listWithUnitsToBuff.Count > 0)
        {
            for (int i = 0; i < listWithUnitsToBuff.Count; i++)
            {
                listWithUnitsToBuff[i].transform.FindChild("BuffHighlighter").gameObject.SetActive(false);
            }
        }
    }


    //Saves the name of the current activated (purchased) buff (sender is the TrackableImageOnScreenHandler class)
    public void currentActivatedBuff(string buffName)
    {
        currentBuffOnScreen = buffName;
    }


    //Return true of false if units are in the buff area (called by the GUIControllerHexa class)
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


    //Updates the list of Units (send by the BuffCollider class)
    public void updateUnitList(List<Unit> unitsList)
    {
        listWithUnitsToBuff = unitsList;
    }


    //Returns the name of the current buff
    public string nameOfTheCurrentBuff()
    {
        return currentBuffOnScreen;
    }


    //Hides the highlighters of the characters which where in the buff area after the buff activation was canceled (called in TrackableImageOnScreenHandler)
    public void hideHighlighterWhenBuffActivationIsCanceled()
    {
        hideHighlightFiledOfCharacter();
    }
}
