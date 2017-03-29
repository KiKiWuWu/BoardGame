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
            Text UnitName = TroopOverview.transform.GetChild(i).FindChild("NameOfUnit").GetComponent<Text>();
            Slider UnitHealthBar = TroopOverview.transform.GetChild(i).FindChild("HPSlider").GetComponent<Slider>();
            Text UnitHealthBarText = TroopOverview.transform.GetChild(i).FindChild("HPText").GetComponent<Text>();
            Image UnitImage = TroopOverview.transform.GetChild(i).FindChild("ImagePlayer").GetComponent<Image>();

            if (i < currentUnits.Count)
            {
                UnitName.text = currentUnits[i].name;
                UnitHealthBar.value = ((float)currentUnits[i].HitPoints / currentUnits[i].TotalHitPoints) * 100;
                UnitHealthBarText.text = "" + currentUnits[i].HitPoints + "/" + currentUnits[i].TotalHitPoints + " HP";
                UnitImage.sprite = Resources.Load<Sprite>(currentUnits[i].name);
                HealthBar.SetActive(true);
            }
            else
            {
                UnitName.text = "Dead";
                UnitHealthBar.value = 0;
                UnitHealthBarText.text = "0 HP";
                UnitImage.sprite = Resources.Load<Sprite>("Dead");
                HealthBar.SetActive(false);
            }
        }
    }
}