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
        if(currentBuffOnScreen == "attack")
        {
            AttackBuff attackBuff = new AttackBuff(0, 10);
            for (int i = 0; i < listWithUnitsToBuff.Count; i++)
            {
                buffSpawner.SpawnBuff(attackBuff, listWithUnitsToBuff[i]);
                listWithUnitsToBuff[i].transform.FindChild("BuffHighlighter").gameObject.SetActive(false);
            }
        }

        listWithUnitsToBuff.Clear();
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
}
