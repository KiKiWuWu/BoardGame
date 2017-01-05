using UnityEngine;
using System.Collections;

public class TestCollider : MonoBehaviour {

    

    void OnTriggerEnter (Collider collidedObject)
    {
        //gameController.playerInAttackRange(collidedObject.transform.parent.name);
        Debug.Log(collidedObject.transform.position);
    }

    void OnTriggerExit (Collider collidedObject)
    {
        //gameController.playerInAttackRange("no Player in range");
        //Debug.Log(collidedObject.name + "Player TWOOOO EXIIIT");
    }
}
