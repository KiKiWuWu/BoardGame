using UnityEngine;

public class CharacterSpecialAttackController : MonoBehaviour {

    private bool wasSpecialAttackSelectedByUser = false;

    private ActionCount actionCount;
    private GUIControllerHexa GUIController;


    void Start()
    {
        actionCount = gameObject.GetComponent<ActionCount>();
        GUIController = gameObject.GetComponent<GUIControllerHexa>();
    }


    //Returns true if the user selected the speacial attack button otherwise returns false (called by ActionCount class)
    public bool specialAttackActivatedByUser()
    {
        return wasSpecialAttackSelectedByUser;
    }


    //(called by SampleUnit and GUIControllerHexa class)
    public void setSpecialAttackButtonToDefault()
    {
        wasSpecialAttackSelectedByUser = false;
    }


    
    //Activates the special attack if there are enought action points. If there are not enought action points a message will be displayed on screen
    public void activateSpecialAttack()
    {
        if (!actionCount.checkRemainingPointsForSpecialAttack())
        {
            GUIController.showInfoScreenThatSpecialAttackIsNotPossible();
        }
        else
        {
            wasSpecialAttackSelectedByUser = !wasSpecialAttackSelectedByUser;
        }
    }
}