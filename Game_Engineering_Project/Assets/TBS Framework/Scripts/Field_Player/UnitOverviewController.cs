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
    public void showAllUnits(string unitsOfPlayer)
    {
        List<Unit> currentUnits = unitController.getCurrentPlayerUnits(unitsOfPlayer);
        int numberOfUnitsToClean = unitController.numberOfUnits() - currentUnits.Count;

        for (int i = 0; i < unitController.numberOfUnits(); i++)
        {
            if (i < currentUnits.Count)
            {
                TroopOverview.transform.GetChild(i).FindChild("NameOfUnit").GetComponent<Text>().text = currentUnits[i].name;
                TroopOverview.transform.GetChild(i).FindChild("HPSlider").GetComponent<Slider>().value = ((float)currentUnits[i].HitPoints / currentUnits[i].TotalHitPoints) * 100;
                TroopOverview.transform.GetChild(i).FindChild("HPText").GetComponent<Text>().text = "" + currentUnits[i].HitPoints + "/" + currentUnits[i].TotalHitPoints + " HP";
            }
            else
            {
                TroopOverview.transform.GetChild(i).FindChild("NameOfUnit").GetComponent<Text>().text = "Dead";
                TroopOverview.transform.GetChild(i).FindChild("HPSlider").GetComponent<Slider>().value = 0;
                TroopOverview.transform.GetChild(i).FindChild("HPText").GetComponent<Text>().text = "0 HP";
            }
        }
    }
}
