﻿using System.Collections;
using UnityEngine;

public class SampleUnit : Unit
{
    private CursorOverPlayerController cursorController;
    private CharacterSpecialAttackController specialAttackContr;

    public Color LeadingColor;



    public override void Initialize()
    {
        base.Initialize();
        transform.position += new Vector3(0, 0, -1);
        GetComponent<Renderer>().material.color = LeadingColor;

        cursorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<CursorOverPlayerController>();
        specialAttackContr = GameObject.FindGameObjectWithTag("GameController").GetComponent<CharacterSpecialAttackController>();
    }

    public override void OnUnitDeselected()
    {
        base.OnUnitDeselected();
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
             
        cursorController.hidePlayerCursor();
        specialAttackContr.setSpecialAttackButtonToDefault();
    }

    public override void MarkAsAttacking(Unit other)
    {
        StartCoroutine(Jerk(other, 0.15f));
    }

    public override void MarkAsDefending(Unit other)
    {
        StartCoroutine(Jerk(other, 0.15f));
    }

    public override void MarkAsDestroyed()
    { 
    }


    public override void MarkAsFinished()
    {
        specialAttackContr.setSpecialAttackButtonToDefault();
    }


    private IEnumerator Jerk(Unit other, float movementTime)
    {
        var heading = other.transform.position - transform.position;
        var direction = heading / heading.magnitude;
        float startTime = Time.time;

        while (true)
        {
            var currentTime = Time.time;
            if (startTime + movementTime < currentTime)
                break;
            transform.position = Vector3.Lerp(transform.position, transform.position + (direction / 2.5f), ((startTime + movementTime) - currentTime));
            yield return 0;
        }
        startTime = Time.time;
        while (true)
        {
            var currentTime = Time.time;
            if (startTime + movementTime < currentTime)
                break;
            transform.position = Vector3.Lerp(transform.position, transform.position - (direction / 2.5f), ((startTime + movementTime) - currentTime));
            yield return 0;
        }
        transform.position = Cell.transform.position + new Vector3(0, 0, -1);
    }



    //Highlights all allies on the field
    public override void MarkAsFriendly()
    {
        //GetComponent<Renderer>().material.color = LeadingColor + new Color(0.8f, 1, 0.8f);
    }


    public override void MarkAsReachableEnemy()
    {
        SetHighlighter("enemy");

        //GetComponent<Renderer>().material.mainTexture = Resources.Load("AttackField") as Texture;
        //GetComponent<Renderer>().material.color = LeadingColor + Color.red;
    }


    public override void MarkAsSelected()
    {
        specialAttackContr.isSpecialAttackPossibleForCharacter(specialAttackPurchased);

        cursorController.showCursorOverCurrentPlayer(this);
    }

    //Removes highlighter from the attackable Units
    public override void UnMark()
    {
        //GetComponent<Renderer>().material.mainTexture = Resources.Load("StandardField") as Texture;
        //GetComponent<Renderer>().material.color = LeadingColor;
        SetHighlighter("unmark");
    }


    private void SetHighlighter(string command)
    {
        var highlighter = transform.Find("HighlighterField").GetComponent<Renderer>();
        if (highlighter != null)
        {
            highlighter.gameObject.SetActive(true);

            if (command == "enemy")
            {
                highlighter.material.mainTexture = Resources.Load("AttackField") as Texture;
            }

            if(command == "unmark")
            {
                highlighter.gameObject.SetActive(false);
            }
        }        
    }
}
