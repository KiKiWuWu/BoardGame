using UnityEngine;

public class MyCastle : MonoBehaviour {

    public int castleOccupied;

    private int neutralState = -1;
    private int occupiedByPlayerOne = 0;
    private int occupiedByPlayerTwo = 1;


    // Use this for initialization
    void Start () {
        castleOccupied = neutralState;
    }
	
	// Update is called once per frame
	void Update () {
        displayColorOfCastle();
	}


    //Highlights the field of the castle with a color depending on the occupation of it 
    private void displayColorOfCastle()
    {
        if(castleOccupied == occupiedByPlayerOne)
        {
            transform.FindChild("playerColor").GetComponent<Renderer>().material.color = Color.blue;
            transform.FindChild("CastleHighlighter").GetComponent<Renderer>().material.color = new Color(0, 0, 1f, 0.8f);
        }
        else if(castleOccupied == occupiedByPlayerTwo)
        {
            transform.FindChild("playerColor").GetComponent<Renderer>().material.color = Color.red;
            transform.FindChild("CastleHighlighter").GetComponent<Renderer>().material.color = new Color(1f, 0, 0, 0.8f);
        }
        else
        {
            transform.FindChild("playerColor").GetComponent<Renderer>().material.color = Color.white;
            transform.FindChild("CastleHighlighter").GetComponent<Renderer>().material.color = new Color(0.8f, 0.8f, 0.8f, 0.8f);
        }
    }


    //Returns the possible actions for the player when he stands on a field with a castle
    public string occupiedStateOfCastle(int currentPlayer)
    {
        string stateOfCastle = ""; 

        if (currentPlayer != castleOccupied)
        {
            stateOfCastle = "neutralize";
        }

        if(castleOccupied == -1)
        {
            stateOfCastle = "conquer";
        }

        if (castleOccupied == currentPlayer)
        {
            stateOfCastle = "conquered";
        }

        return stateOfCastle;
    }


    //Returns the occupation state of the castle
    public int castleOccupation()
    {
        return castleOccupied;
    }


    //changes the occupation state of the castle
    public void changeStateOfCastle(int fraction)
    {
        if(fraction == neutralState)
        {
            castleOccupied = neutralState;
        }

        if(fraction == 0)
        {
            castleOccupied = occupiedByPlayerOne;
        }

        if(fraction == 1)
        {
            castleOccupied = occupiedByPlayerTwo;
        }
    }
}