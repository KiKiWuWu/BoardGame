using UnityEngine;

public class CharacterSpecialAttackController : MonoBehaviour
{
    private bool wasSpecialAttackSelectedByUser = false;

    private ActionCount actionCount;
    private MessagesOnScreenController messageOnScreenController;


    //Called when the application is started
    void Start()
    {
        actionCount = gameObject.GetComponent<ActionCount>();
        messageOnScreenController = gameObject.GetComponent<MessagesOnScreenController>();
    }


    //Returns true if the user selected the speacial attack button otherwise returns false (called by ActionCount.cs)
    public bool specialAttackActivatedByUser()
    {
        return wasSpecialAttackSelectedByUser;
    }


    //Sets the state if special attack is currently active to false (called by SampleUnit.cs and GUIControllerHexa.cs)
    public void setSpecialAttackButtonToDefault()
    {
        wasSpecialAttackSelectedByUser = false;
    }


    //Activates the special attack if there are enought action points. If there are not enought action points a message will be displayed on screen
    public void activateSpecialAttack()
    {
        if (!actionCount.checkRemainingPointsForSpecialAttack())
        {
            messageOnScreenController.showSpecialAttackNotPossibleScreen();
        }
        else
        {
            wasSpecialAttackSelectedByUser = !wasSpecialAttackSelectedByUser;
        }
    }
}