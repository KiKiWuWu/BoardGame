using UnityEngine;

public class FieldPositionController : MonoBehaviour
{
    public GameObject GameField;

    private Vector3 standardFieldPosition = new Vector3(0, 0, 0);
    private Quaternion standardFieldRotation = Quaternion.Euler(-90, 0, 0);


    // Update is called once per frame
    //If the image target was captured wrong, the standard position will be set automatically
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
