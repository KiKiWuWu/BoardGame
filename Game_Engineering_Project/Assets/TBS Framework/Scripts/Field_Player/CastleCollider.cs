using UnityEngine;

public class CastleCollider : MonoBehaviour {

    //Checks if the collider hits a playable character, inserts the character in a list, shows a highligter on the floor and updates the list of units in the buffExecuter class
    void OnTriggerEnter(Collider collidedObject)
    {
        if (collidedObject.tag == "playableCharacter")
        {
            print(this);
            print("PLAYER ENTERRR");
            var unit = GameObject.Find(collidedObject.transform.name).GetComponent<Unit>();
            unit.standOnCastle = this.GetComponent<MyCastle>();
            //collidedObject.transform.FindChild("BuffHighlighter").gameObject.SetActive(true);
        }
    }


    //Checks if the collider doesn´t collide with a playable character anymore, removes the unit from the list, hides the highligter of that character and updates the list in the buffExecuter class
    void OnTriggerExit(Collider collidedObject)
    {
        if (collidedObject.tag == "playableCharacter")
        {
            print("PLAYYEERRR EXITTT");
            print(collidedObject);
            var unit = GameObject.Find(collidedObject.transform.name).GetComponent<Unit>();
            unit.standOnCastle = null;
            //collidedObject.transform.FindChild("BuffHighlighter").gameObject.SetActive(false);
        }
    }
}
