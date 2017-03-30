using UnityEngine;

public class NoButtonsPressableController : MonoBehaviour
{ 
    public GameObject EndTurnButtonArea;

    AllUnitsController unitController;


    //Called when the application is started
    void Start()
    {
        unitController = gameObject.GetComponent<AllUnitsController>();
    }


    //Called every frame
    //Checks if player one or player two is currently activ ad displays or hides the end turn button
    void Update()
    {
        if(unitController.activePlayer() == 0)
        {
            EndTurnButtonArea.SetActive(true);
        }
        else
        {
            EndTurnButtonArea.SetActive(false);
        }
    }
}