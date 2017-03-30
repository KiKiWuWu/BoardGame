using UnityEngine;
using System.Collections.Generic;

public class BuffCollider : MonoBehaviour
{
    private List<Unit> unitsList = new List<Unit>();
    private BuffExecuter buffExecuter;


    //Is called on start
    void Start()
    {
        buffExecuter = GameObject.FindGameObjectWithTag("GameController").GetComponent<BuffExecuter>();
    }


    //Checks if the collider hits a playable character, inserts the character in a list, shows a highligter on the floor and updates the list of units in the BuffExecuter.cs
    void OnTriggerEnter(Collider collidedObject)
    {
        if(collidedObject.tag == "playableCharacter" && collidedObject.transform.FindChild("BuffHighlighter") != null)
        {
            var unit = GameObject.Find(collidedObject.transform.name).GetComponent<Unit>();
            unitsList.Add(unit);
            updateListOfUnits();
            collidedObject.transform.FindChild("BuffHighlighter").gameObject.SetActive(true);
        }
    }


    //Checks if the collider doesn´t collide with a playable character anymore, removes the unit from the list, hides the highligter of that character and updates the list in the BuffExecuter.cs
    void OnTriggerExit (Collider collidedObject)
    {
        if (collidedObject.tag == "playableCharacter" && collidedObject.transform.FindChild("BuffHighlighter") != null)
        {
            var unit = GameObject.Find(collidedObject.transform.name).GetComponent<Unit>();
            unitsList.Remove(unit);
            updateListOfUnits();
            collidedObject.transform.FindChild("BuffHighlighter").gameObject.SetActive(false);
        }
    }

    
    //Updates the list of units in the BuffExecuter.cs
    private void updateListOfUnits()
    {
        buffExecuter.updateUnitList(unitsList);
    }
}