using UnityEngine;
using System.Collections;

public class MainGameControls : MonoBehaviour {

    public GameObject PlayerOne;
    public GameObject PlayerTwo;
    public GameObject Canvas;

    private float scaleNumb = 3;
    private float numberOfFields = 4;

    private bool playerTurn = true;
    private string nameOfPlayer = "";

    private int playerMovementsLeft = 1;
    private int playerAttacksLeft = 1;

    private int lifePlayerOne;
    private int lifePlayerTwo;

    private GameObject currentPlayer;


    // Use this for initialization
    void Start() {
        initializePlayersLife();
        selectPlayer();
        showPlayerOneLive();
        showPlayerTwoLive();


        Debug.Log(PlayerOne.transform.position);
        Debug.Log(PlayerTwo.transform.position);
    }


    private void initializePlayersLife()
    {
        lifePlayerOne = 4;
        lifePlayerTwo = 4;
    }

    private void selectPlayer()
    {
        if (playerTurn)
        {
            currentPlayer = PlayerOne;

            PlayerOne.transform.FindChild("MovementDirections").gameObject.SetActive(true);
            PlayerOne.transform.FindChild("ActiveChar").gameObject.SetActive(true);
            PlayerOne.transform.FindChild("ColliderPlayerOne").gameObject.SetActive(true);
            PlayerTwo.transform.FindChild("MovementDirections").gameObject.SetActive(false);
            PlayerTwo.transform.FindChild("ActiveChar").gameObject.SetActive(false);
            PlayerTwo.transform.FindChild("ColliderPlayerTwo").gameObject.SetActive(false);
            Canvas.transform.FindChild("PlayerTurnOnScreen").FindChild("CurrentPlayer").FindChild("PlayerOneOnScreen").gameObject.SetActive(true);
            Canvas.transform.FindChild("PlayerTurnOnScreen").FindChild("CurrentPlayer").FindChild("PlayerTwoOnScreen").gameObject.SetActive(false);

        }
        else
        {
            currentPlayer = PlayerTwo;

            PlayerTwo.transform.FindChild("MovementDirections").gameObject.SetActive(true);
            PlayerTwo.transform.FindChild("ActiveChar").gameObject.SetActive(true);
            PlayerTwo.transform.FindChild("ColliderPlayerTwo").gameObject.SetActive(true);
            PlayerOne.transform.FindChild("MovementDirections").gameObject.SetActive(false);
            PlayerOne.transform.FindChild("ActiveChar").gameObject.SetActive(false);
            PlayerOne.transform.FindChild("ColliderPlayerOne").gameObject.SetActive(false);
            Canvas.transform.FindChild("PlayerTurnOnScreen").FindChild("CurrentPlayer").FindChild("PlayerOneOnScreen").gameObject.SetActive(false);
            Canvas.transform.FindChild("PlayerTurnOnScreen").FindChild("CurrentPlayer").FindChild("PlayerTwoOnScreen").gameObject.SetActive(true);
        }
    }



    private void showPlayerOneLive()
    {
        PlayerOne.transform.FindChild("LifeOfPlayer").FindChild("Life4").gameObject.SetActive(false);
        PlayerOne.transform.FindChild("LifeOfPlayer").FindChild("Life3").gameObject.SetActive(false);
        PlayerOne.transform.FindChild("LifeOfPlayer").FindChild("Life2").gameObject.SetActive(false);
        PlayerOne.transform.FindChild("LifeOfPlayer").FindChild("Life1").gameObject.SetActive(false);
        if (lifePlayerOne == 4)
        {
            PlayerOne.transform.FindChild("LifeOfPlayer").FindChild("Life4").gameObject.SetActive(true);
        }
        else if(lifePlayerOne == 3)
        {
            PlayerOne.transform.FindChild("LifeOfPlayer").FindChild("Life3").gameObject.SetActive(true);
        }
        else if (lifePlayerOne == 2)
        {
            PlayerOne.transform.FindChild("LifeOfPlayer").FindChild("Life2").gameObject.SetActive(true);
        }
        else if (lifePlayerOne == 1)
        {
            PlayerOne.transform.FindChild("LifeOfPlayer").FindChild("Life1").gameObject.SetActive(true);
        }
    }


    private void showPlayerTwoLive()
    {
        PlayerTwo.transform.FindChild("LifeOfPlayer").FindChild("Life4").gameObject.SetActive(false);
        PlayerTwo.transform.FindChild("LifeOfPlayer").FindChild("Life3").gameObject.SetActive(false);
        PlayerTwo.transform.FindChild("LifeOfPlayer").FindChild("Life2").gameObject.SetActive(false);
        PlayerTwo.transform.FindChild("LifeOfPlayer").FindChild("Life1").gameObject.SetActive(false);
        if (lifePlayerTwo == 4)
        {
            PlayerTwo.transform.FindChild("LifeOfPlayer").FindChild("Life4").gameObject.SetActive(true);
        }
        else if (lifePlayerTwo == 3)
        {
            PlayerTwo.transform.FindChild("LifeOfPlayer").FindChild("Life3").gameObject.SetActive(true);
        }
        else if (lifePlayerTwo == 2)
        {
            PlayerTwo.transform.FindChild("LifeOfPlayer").FindChild("Life2").gameObject.SetActive(true);
        }
        else if (lifePlayerTwo == 1)
        {
            PlayerTwo.transform.FindChild("LifeOfPlayer").FindChild("Life1").gameObject.SetActive(true);
        }
    }



    public void showMovementDirections(int player)
    {        
        checkShowableMoveDirections();
    }


    private void checkShowableMoveDirections()
    {
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
        playerMovementsLeft--;
    }


    public void changeTurnOfPlayer()
    {
        removeMoveIndikator();
        nameOfPlayer = "";
        playerMovementsLeft = 1;
        playerAttacksLeft = 1;
        playerTurn = !playerTurn;
        selectPlayer();
        checkAttackRangeOfPlayers();
        showNextPlayerScreen();
    }


    private void showNextPlayerScreen()
    {
        Canvas.transform.FindChild("ChangeTurnScreen").gameObject.SetActive(true);

        Invoke("hideTurnScreen", 1);
    }

    void hideTurnScreen()
    {
        Canvas.transform.FindChild("ChangeTurnScreen").gameObject.SetActive(false);
    }


    public void playerInAttackRange(string playerInRangeName)
    {
        nameOfPlayer = playerInRangeName;
        checkAttackRangeOfPlayers();
    }


    private void checkAttackRangeOfPlayers()
    {
        if(nameOfPlayer == PlayerOne.name && playerAttacksLeft > 0)
        {
            PlayerOne.transform.FindChild("AttackableChar").gameObject.SetActive(true);
        }
        else if(nameOfPlayer == PlayerTwo.name && playerAttacksLeft > 0)
        {
            PlayerTwo.transform.FindChild("AttackableChar").gameObject.SetActive(true);
        }
        else
        {
            PlayerOne.transform.FindChild("AttackableChar").gameObject.SetActive(false);
            PlayerTwo.transform.FindChild("AttackableChar").gameObject.SetActive(false);
        }
    }


    public void attackPlayer(int player)
    {
        int temp = Random.Range(1, 10);
        int damage;
        if (temp == 2 || temp == 5)
        {
            damage = 2;
        }
        else if (temp == 9)
        {
            damage = 3;
        }
        else
        {
            damage = 1;
        }
        


        if(player == 1 && nameOfPlayer == PlayerTwo.name && playerAttacksLeft > 0)
        {
            lifePlayerTwo -= damage;
            playerAttacksLeft--;
            showPlayerTwoLive();
        }
        else if(player == 0 && nameOfPlayer == PlayerOne.name && playerAttacksLeft > 0)
        {
            lifePlayerOne -= damage;
            playerAttacksLeft--;
            showPlayerOneLive();
        }
        checkAttackRangeOfPlayers();
        checkStateOfGame();
    }


    private void checkStateOfGame()
    {
        if(lifePlayerOne <= 0 || lifePlayerTwo <= 0)
        {
            Canvas.transform.FindChild("EndScreen").gameObject.SetActive(true);
            //EndScreen.SetActive(true);
        }
    }



    public bool getCurrentTurn()
    {
        return playerTurn;
    }


    public int getNumberOfMovements()
    {
        return playerMovementsLeft;
    }
}
