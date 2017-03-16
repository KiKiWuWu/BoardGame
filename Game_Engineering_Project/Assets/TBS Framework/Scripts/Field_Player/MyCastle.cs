using System;
using System.Collections;
using System.Collections.Generic;
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



    private void displayColorOfCastle()
    {
        if(castleOccupied == occupiedByPlayerOne)
        {
            transform.FindChild("playerColor").GetComponent<Renderer>().material.color = Color.blue;
        }
        else if(castleOccupied == occupiedByPlayerTwo)
        {
            transform.FindChild("playerColor").GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            transform.FindChild("playerColor").GetComponent<Renderer>().material.color = Color.white;
        }
    }


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


    public int castleOccupation()
    {
        return castleOccupied;
    }


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
