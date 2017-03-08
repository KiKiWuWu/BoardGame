using UnityEngine;

public class GoldController : MonoBehaviour {

    private int startAmountOfGold = 50;
    private int amountPlayersGetEveryTurn = 10;
    private int costToActivateABuff = 200;
    private int costToUnlockSpecialAttack = 500;

    private int goldCountPlayerOne;
    private int goldCountPlayerTwo;

    private AllUnitsController unitController;


	// Use this for initialization
	void Start () {
        unitController = gameObject.GetComponent<AllUnitsController>();
        goldCountPlayerOne = startAmountOfGold;
        goldCountPlayerTwo = startAmountOfGold;
	}


    //Returns the amount of gold that the current player posseses (called by GUIController class)
    public int getGoldCountOfActivePlayer()
    {
        if (unitController.activePlayer() == 0)
        {
            return goldCountPlayerOne;
        }
        else
        {
            return goldCountPlayerTwo;
        }
    }


    //Adds a specific amount of gold to the gold count of the current player (called by GUIController class)
    public void addGoldToPlayersGoldCount()
    {
        if(unitController.activePlayer() == 0)
        {
            goldCountPlayerOne += amountPlayersGetEveryTurn;
        }
        else
        {
            goldCountPlayerTwo += amountPlayersGetEveryTurn;
        }
    }


    //Checks if it is possible to purchase the special ability of a charachter
    public bool isSpecialAttackPurchasePossible()
    {
        if(unitController.activePlayer() == 0 && goldCountPlayerOne >= costToUnlockSpecialAttack)
        {
            return true;
        }
        else if(unitController.activePlayer() == 1 && goldCountPlayerTwo >= costToUnlockSpecialAttack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //Checks if the current player has enought gold to activate a buff
    public bool isPurchaseOfABuffPossible()
    {
        if (unitController.activePlayer() == 0 && goldCountPlayerOne >= costToActivateABuff)
        {
            return true;
        }
        else if (unitController.activePlayer() == 1 && goldCountPlayerTwo >= costToActivateABuff)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //Reduces the gold of the current player after the special attack was unlocked
    public void reduceGoldAfterAPurchase(string purchased)
    {
        int costOfPurchase = 0;
        if(purchased == "specialAttack")
        {
            costOfPurchase = costToUnlockSpecialAttack;
        }

        if(purchased == "buff")
        {
            costOfPurchase = costToActivateABuff;
        }
        executePurchase(costOfPurchase);
    }


    //Reduces the gold count of current active player
    private void executePurchase(int costOfPurchase)
    {
        if (unitController.activePlayer() == 0)
        {
            goldCountPlayerOne -= costOfPurchase;
        }
        else
        {
            goldCountPlayerTwo -= costOfPurchase;
        }
    }
}
