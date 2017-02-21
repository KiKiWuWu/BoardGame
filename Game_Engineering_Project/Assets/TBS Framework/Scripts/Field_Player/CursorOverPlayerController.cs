using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorOverPlayerController : MonoBehaviour {

    public GameObject CursorOverPlayer;

    private float spinSpeed = 275f;
    private bool playerSelected = false;



    void Update () {
        if (playerSelected)
        {
            spinCursorOverCharacterHead();
        }
	}


    private void spinCursorOverCharacterHead()
    {
        CursorOverPlayer.transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
    }



    public void hidePlayerCursor()
    {
        CursorOverPlayer.SetActive(false);
        playerSelected = false;
    }



    public void showCursorOverCurrentPlayer(SampleUnit sampleUnit)
    {
        playerSelected = true;
        Transform cursorPositionOfSelectedChar = sampleUnit.transform.FindChild("CenterOverCharacterHead");
        CursorOverPlayer.transform.parent = cursorPositionOfSelectedChar.transform;
        CursorOverPlayer.transform.position = cursorPositionOfSelectedChar.transform.position;
        CursorOverPlayer.SetActive(true);
    }
}
