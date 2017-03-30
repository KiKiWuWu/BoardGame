using UnityEngine;

public class CastleCollider : MonoBehaviour
{

    //Saves the castle object in the unit object when a unit is standing on a field with a castle
    void OnTriggerEnter(Collider collidedObject)
    {
        if (collidedObject.tag == "playableCharacter")
        {
            var unit = GameObject.Find(collidedObject.transform.name).GetComponent<Unit>();
            unit.standOnCastle = GetComponent<MyCastle>();
        }
    }


    //Removes the castle object in the unit object when a unit is leaving the field where a castle is on
    void OnTriggerExit(Collider collidedObject)
    {
        if (collidedObject.tag == "playableCharacter")
        {
            var unit = GameObject.Find(collidedObject.transform.name).GetComponent<Unit>();
            unit.standOnCastle = null;
        }
    }
}