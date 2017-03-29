using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDestroyerController : MonoBehaviour
{
    public GameObject CellGridGameObject;

    private int turnToDestroyLevel1 = 103;
    private int turnToDestroyLevel2 = 107;
    private int turnToDestroyLevel3 = 111;
    private int turnToDestroyLevel4 = 115;
    private int turnToDestroyLevel5 = 119;

    private int numberOfTurnsToWarnPlayer = 2;
    private int levelToDestroyCounter = 1;

    private MessagesOnScreenController messageOnScreenController;
    private TurnCounter turnCount;
        

	// Use this for initialization
	void Start () {
        messageOnScreenController = gameObject.GetComponent<MessagesOnScreenController>();
        turnCount = gameObject.GetComponent<TurnCounter>();
	}


    //Checks the number of the current turn and if it´s nesessery highlights the fields which will be destroyed next or destroys the fields
    public void checkDestructionOfField()
    {
        if(turnCount.currentTurn() >= (turnToDestroyLevel1 - numberOfTurnsToWarnPlayer))
        {
            int levelToDestroy = 0;

            if (levelToDestroyCounter == 1)
            {
                levelToDestroy = turnToDestroyLevel1;
            }
            else if (levelToDestroyCounter == 2)
            {
                levelToDestroy = turnToDestroyLevel2;
            }
            else if (levelToDestroyCounter == 3)
            {
                levelToDestroy = turnToDestroyLevel3;
            }
            else if (levelToDestroyCounter == 4)
            {
                levelToDestroy = turnToDestroyLevel4;
            }
            else if (levelToDestroyCounter == 5)
            {
                levelToDestroy = turnToDestroyLevel5;
            }

            
            if (turnCount.currentTurn() >= (levelToDestroy - numberOfTurnsToWarnPlayer) && turnCount.currentTurn() < levelToDestroy)
            {
                highlightFieldWhichWillBeDestroyed();
                messageOnScreenController.showFieldCrumbleMessageScreen(levelToDestroy - turnCount.currentTurn());
            }
            else if (turnCount.currentTurn() == levelToDestroy)
            {
                destroySelectedFields();
                messageOnScreenController.showFieldCrumbleMessageScreen(0);
            }
        }
    }


    //Highlights all the fields which will crumble next
    private void highlightFieldWhichWillBeDestroyed()
    {
        int childsInCellGrid = CellGridGameObject.transform.childCount;
        
        for (int i = childsInCellGrid - 1; i > 0; i--)
        {
            if (CellGridGameObject.transform.GetChild(i).tag == "level" + levelToDestroyCounter)
            {
                if(CellGridGameObject.transform.GetChild(i).FindChild("HexagonHighlighter") != null)
                {
                    CellGridGameObject.transform.GetChild(i).FindChild("HexagonHighlighter").gameObject.SetActive(true);
                    StartCoroutine(hideHighlighter(CellGridGameObject.transform.GetChild(i).FindChild("HexagonHighlighter").gameObject));
                }
            }
        }
    }


    //A coroutine that shows the highlights on the field for 2.5 seconds and then hides the field highlighter
    private IEnumerator hideHighlighter(GameObject gameObject)
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(1f, 0.65f, 0, 0.6f);

        yield return new WaitForSeconds(2.5f);
        
        gameObject.SetActive(false);
    }


    //Destroys a group of fields depending on the number of turns
    private void destroySelectedFields()
    {
        int childsInCellGrid = CellGridGameObject.transform.childCount;
        List<Unit> listOfUnitsToDestroy = new List<Unit>();
        for (int i = childsInCellGrid - 1; i > 0; i--)
        {
            if(CellGridGameObject.transform.GetChild(i).tag == "level" + levelToDestroyCounter)
            {
                if(CellGridGameObject.transform.GetChild(i).GetComponent<MyHexagon>().unitOnCell != null)
                {
                    listOfUnitsToDestroy.Add(CellGridGameObject.transform.GetChild(i).GetComponent<MyHexagon>().unitOnCell);
                }
                StartCoroutine(moveObjectThenHideOrDestroy(CellGridGameObject.transform.GetChild(i).gameObject));
            }
        }
        checkDestructionOfUnit(listOfUnitsToDestroy);
        levelToDestroyCounter++;
    }


    //A coroutine that moves the fields (and the characters on those fields) to a specific position and the hides the fields 
    private IEnumerator moveObjectThenHideOrDestroy(GameObject gameObject)
    {
        Vector3 targetPosition = gameObject.transform.position;
        targetPosition.z += 5.5f;

        while(Vector3.Distance(gameObject.transform.position, targetPosition) > 0.4f)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition, Time.deltaTime * 1f);

            yield return null;
        }
        if(gameObject.tag != "playableCharacter")
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.GetComponent<Unit>().destroyUnit();
        }
    }


    //Checks if a unit stands on a field that is destroyed and either destroys the units or sends a message if all units are destroyed in the same round 
    private void checkDestructionOfUnit(List<Unit> unitList)
    {
        if(GameObject.Find("Unit").transform.childCount == unitList.Count)
        {
            StartCoroutine(moveUnitsAndShowEndScreen(unitList));
        }
        else
        {
            for(int i = 0; i < unitList.Count; i++)
            {
                StartCoroutine(moveObjectThenHideOrDestroy(unitList[i].gameObject));
            }
        }
    }


    //A couroutine that moves all remaining units to a specific position and sends a message that the game ends in a draw
    private IEnumerator moveUnitsAndShowEndScreen(List<Unit> unitList)
    {
        Vector3 targetPosition = gameObject.transform.position;
        targetPosition.z += 5.8f;

        var units = new GameObject();
        
        for(int i = 0; i < unitList.Count; i++)
        {
            unitList[i].transform.parent = units.transform;
        }

        while (Vector3.Distance(units.transform.position, targetPosition) > 0.4f)
        {
            units.transform.position = Vector3.Lerp(units.transform.position, targetPosition, Time.deltaTime * 1f);

            yield return null;
        }

        units.SetActive(false);
        CellGridGameObject.GetComponent<CellGrid>().EndGameWhenDraw();
    }


    //Returns the turn number when the destruction of the fields is starting
    public int startFieldDestructionTurn()
    {
        return turnToDestroyLevel1;
    }
}