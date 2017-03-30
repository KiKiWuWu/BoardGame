using UnityEngine;

public class CursorOverPlayerController : MonoBehaviour {

    public GameObject CursorStandartPosition;
    public GameObject CursorOverPlayer;
    public GameObject CursorOverEnemy;

    private float spinSpeed = 275f;
    private bool playerSelected = false;
    private bool enemySelected = false;


    void Update () {
        if (playerSelected)
        {
            spinCursorOverCharacterHead("friend");
        }

        if (enemySelected)
        {
            spinCursorOverCharacterHead("enemy");
        }
	}


    //Spins the shown cursor either over the selected friend or foe unit
    private void spinCursorOverCharacterHead(string unit)
    {
        if(unit == "friend")
        {
            CursorOverPlayer.transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
        }
        else
        {
            CursorOverEnemy.transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
        }
    }


    //Hides the displayed cursor either of the selected frieds or foes unit
    public void hidePlayerCursor(string unit)
    {
        if(unit == "friend")
        {
            CursorOverPlayer.transform.parent = CursorStandartPosition.transform;
            CursorOverPlayer.transform.position = CursorStandartPosition.transform.position;

            playerSelected = false;
            CursorOverPlayer.SetActive(false);
        }
        else
        {
            CursorOverEnemy.transform.parent = CursorStandartPosition.transform;
            CursorOverEnemy.transform.position = CursorStandartPosition.transform.position;

            enemySelected = false;
            CursorOverEnemy.SetActive(false);
        }
    }


    //Shows the cursor over the head of a selected friend or foe unit
    public void showCursorOverSelectedUnit(string unitFraction, Unit sampleUnit)
    {
        GameObject SelectedChar;

        if (unitFraction == "friend")
        {
            playerSelected = true;
            SelectedChar = CursorOverPlayer;
        }
        else
        {
            enemySelected = true;
            SelectedChar = CursorOverEnemy;
        }
        
        Transform cursorPositionOfSelectedChar = sampleUnit.transform.FindChild("CenterOverCharacterHead");
        SelectedChar.transform.parent = cursorPositionOfSelectedChar.transform;
        SelectedChar.transform.position = cursorPositionOfSelectedChar.transform.position;
        SelectedChar.SetActive(true);
    }
}