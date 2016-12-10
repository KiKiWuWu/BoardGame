using UnityEngine;
using System.Collections;

public class AttackCollider : MonoBehaviour {

    public MainGameControls gameController;

    void OnTriggerEnter (Collider collidedObject)
    {
        gameController.playerInAttackRange(collidedObject.transform.parent.name);
    }

    void OnTriggerExit (Collider collidedObject)
    {
        gameController.playerInAttackRange("no Player in range");
        Debug.Log(collidedObject.name + "Player TWOOOO EXIIIT");
    }
}
