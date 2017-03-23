using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitOverviewController : MonoBehaviour
{
    public GameObject TroopOverview;

    private AllUnitsController unitController;

	// Use this for initialization
	void Start () {
        unitController = gameObject.GetComponent<AllUnitsController>();
	}


    //Shows all friend or foe units in the overview screen
    public void showAllUnits(string unitsToDisplay)
    {
        int playersUnits;
        if(unitsToDisplay == "friends")
        {
            playersUnits = unitController.activePlayer();
        }
        else
        {
            if(unitController.activePlayer() == 0)
            {
                playersUnits = 1;
            }
            else
            {
                playersUnits = 0;
            }
        }

        List<Unit> currentUnits = unitController.getCurrentPlayerUnits(playersUnits);

        for (int i = 0; i < unitController.numberOfUnits(); i++)
        {
            GameObject HealthBar = TroopOverview.transform.GetChild(i).FindChild("HPSlider").transform.FindChild("Fill Area").gameObject;
            if (i < currentUnits.Count)
            {
                TroopOverview.transform.GetChild(i).FindChild("NameOfUnit").GetComponent<Text>().text = currentUnits[i].name;
                TroopOverview.transform.GetChild(i).FindChild("HPSlider").GetComponent<Slider>().value = ((float)currentUnits[i].HitPoints / currentUnits[i].TotalHitPoints) * 100;
                TroopOverview.transform.GetChild(i).FindChild("HPText").GetComponent<Text>().text = "" + currentUnits[i].HitPoints + "/" + currentUnits[i].TotalHitPoints + " HP";
                HealthBar.SetActive(true);
            }
            else
            {
                TroopOverview.transform.GetChild(i).FindChild("NameOfUnit").GetComponent<Text>().text = "Dead";
                TroopOverview.transform.GetChild(i).FindChild("HPSlider").GetComponent<Slider>().value = 0;
                TroopOverview.transform.GetChild(i).FindChild("HPText").GetComponent<Text>().text = "0 HP";
                HealthBar.SetActive(false);
            }
        }
    }
}