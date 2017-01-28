using UnityEngine;
using System.Collections.Generic;

public class TestCollider : MonoBehaviour {

    private List<Unit> unitsList = new List<Unit>();


    public bool objectsInBuffArea = false;


    /*
    void Update()
    {
        if(unitsList.Count > 0)
        {
            Debug.Log("MOOOORRREEEEEE");
        }
        else
        {
            Debug.Log("LEEEEESSSSSSSS");
        }
    }

    */

    void OnTriggerEnter(Collider collidedObject)
    {
        var unit = GameObject.Find(collidedObject.transform.name).GetComponent<Unit>();
        if (!unitsList.Contains(unit))
        {
            unitsList.Add(unit);
        }
        
        //Debug.Log(collidedObject.transform.name);
        //Debug.Log("TRIGGERRRRRRRRRRRRRRRR Enter");
        Debug.Log("UNIT ADDDEEEEDDDD");
    }

    void OnTriggerExit (Collider collidedObject)
    {
        var unit = GameObject.Find(collidedObject.transform.name).GetComponent<Unit>();
        if (unitsList.Contains(unit))
        {
            unitsList.Remove(unit);
        }

        //Debug.Log("TRIGGERRRRRRRRRRRRRRRR Exit");
        //Debug.Log(collidedObject.transform.name);
        Debug.Log("UNIT REMOVEEEEDDDDD");
    }


    public List<Unit> getUnitsForBuff()
    {
        return unitsList;
    }
}
