using UnityEngine;
using System.Collections;

public class AttackCollider : MonoBehaviour {

    void OnTriggerEnter (Collider collidedObject)
    {
        Debug.Log(collidedObject.name + "Player TWOOOO IS HERE");
    }

    void OnTriggerExit (Collider collidedObject)
    {
        Debug.Log(collidedObject + "Player TWOOOO EXIIIT");
    }
}
