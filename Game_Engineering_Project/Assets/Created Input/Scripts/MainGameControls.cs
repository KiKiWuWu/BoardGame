using UnityEngine;
using System.Collections;

public class MainGameControls : MonoBehaviour {

    public GameObject PlayerOne;
    public GameObject PlayerTwo;

    private float scaleNumb = 3;
    private float numberOfFields = 4;
    private bool playerTurn = true;

    private GameObject currentPlayer;


    // Use this for initialization
    void Start() {
        selectPlayer();


        Debug.Log(PlayerOne.transform.position);
        Debug.Log(PlayerTwo.transform.position);
    }

    private void selectPlayer()
    {
        if (playerTurn)
        {
            PlayerOne.transform.FindChild("MovementDirections").gameObject.SetActive(true);
            PlayerOne.transform.FindChild("ActiveChar").gameObject.SetActive(true);
            PlayerTwo.transform.FindChild("MovementDirections").gameObject.SetActive(false);
            PlayerTwo.transform.FindChild("ActiveChar").gameObject.SetActive(false);
        }
        else
        {
            PlayerTwo.transform.FindChild("MovementDirections").gameObject.SetActive(true);
            PlayerTwo.transform.FindChild("ActiveChar").gameObject.SetActive(true);
            PlayerOne.transform.FindChild("MovementDirections").gameObject.SetActive(false);
            PlayerOne.transform.FindChild("ActiveChar").gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update () {
	
	}


    public void showMovementDirections(int player)
    {
        if (player == 0)
        {
            currentPlayer = PlayerOne;
        }
        else
        {
            currentPlayer = PlayerTwo;
        }
        
        checkShowableMoveDirections();
    }


    private void checkShowableMoveDirections()
    {
        Debug.Log(currentPlayer.transform.position);
        Debug.Log(numberOfFields * scaleNumb);
        if (currentPlayer.transform.position.x > 0)
        {
            currentPlayer.transform.FindChild("MovementDirections").transform.FindChild("FloorLeft").gameObject.SetActive(true);
        }
        if (currentPlayer.transform.position.x < numberOfFields * scaleNumb)
        {
            currentPlayer.transform.FindChild("MovementDirections").transform.FindChild("FloorRight").gameObject.SetActive(true);
        }
        if(currentPlayer.transform.position.z > 0)
        {
            currentPlayer.transform.FindChild("MovementDirections").transform.FindChild("FloorBottom").gameObject.SetActive(true);
        }
        if(currentPlayer.transform.position.z < numberOfFields * scaleNumb)
        {
            currentPlayer.transform.FindChild("MovementDirections").transform.FindChild("FloorTop").gameObject.SetActive(true);
        }
    }

    public void removeMoveIndikator()
    {
        currentPlayer.transform.FindChild("MovementDirections").transform.FindChild("FloorLeft").gameObject.SetActive(false);
        currentPlayer.transform.FindChild("MovementDirections").transform.FindChild("FloorRight").gameObject.SetActive(false);
        currentPlayer.transform.FindChild("MovementDirections").transform.FindChild("FloorBottom").gameObject.SetActive(false);
        currentPlayer.transform.FindChild("MovementDirections").transform.FindChild("FloorTop").gameObject.SetActive(false);
    }



    public void moveSelectedCharacter(int direction)
    {
        Vector3 moveDirection = new Vector3(0, 0, 0);
        switch (direction)
        {
            case 0:
                moveDirection.z += scaleNumb;
                break;
            case 1:
                moveDirection.x += scaleNumb;
                break;
            case 2:
                moveDirection.z -= scaleNumb;
                break;
            case 3:
                moveDirection.x -= scaleNumb;
                break;
        }
        

        if(PlayerOne == currentPlayer)
        {
            PlayerOne.transform.position += moveDirection;
        }
        else
        {
            PlayerTwo.transform.position += moveDirection;
        }
    }


    public void changeTurnOfPlayer()
    {
        removeMoveIndikator();
        playerTurn = !playerTurn;
        selectPlayer();
    }
    
    public bool getCurrentTurn()
    {
        return playerTurn;
    }
    
}
