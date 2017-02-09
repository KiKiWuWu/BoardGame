using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldPositionController : MonoBehaviour
{
    public GameObject GameField;

    private Vector3 standardFieldPosition = new Vector3(0, 0, 0);
    private Quaternion standardFieldRotation = Quaternion.Euler(-90, 0, 0);

	
	// Update is called once per frame
	void Update () {
		if(GameField.transform.position != standardFieldPosition)
        {
            GameField.transform.position = standardFieldPosition;
        }

        if(GameField.transform.rotation != standardFieldRotation)
        {
            GameField.transform.rotation = standardFieldRotation;
        }
	}
}
