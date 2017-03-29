using UnityEngine;
using UnityEngine.UI;

public class CharacterStatusController : MonoBehaviour
{
    public GameObject StatusAlliedUnit;
    public GameObject StatusEnemyUnit;

    private AllUnitsController unitController;


	// Use this for initialization
	void Start () {
        unitController = gameObject.GetComponent<AllUnitsController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (unitController.currentlySelectedAlliedUnit() != null)
        {
            StatusAlliedUnit.transform.FindChild("ATKText").GetComponent<Text>().text = "ATK: " + unitController.currentlySelectedAlliedUnit().AttackFactor;
            StatusAlliedUnit.transform.FindChild("DEFText").GetComponent<Text>().text = "DEF: " + unitController.currentlySelectedAlliedUnit().DefenceFactor;
            StatusAlliedUnit.transform.FindChild("MOVText").GetComponent<Text>().text = "MOV: " + unitController.currentlySelectedAlliedUnit().MovementPoints;
            StatusAlliedUnit.transform.FindChild("SP.ATKText").GetComponent<Text>().text = "SP.ATK: " + (unitController.currentlySelectedAlliedUnit().AttackFactor + unitController.getAdditionalAttackPointsWhenSpecialAttack());
            StatusAlliedUnit.transform.FindChild("ATK.RANText").GetComponent<Text>().text = "ATK.RAN " + unitController.currentlySelectedAlliedUnit().AttackRange;

            StatusAlliedUnit.SetActive(true);
        }
        else
        {
            StatusAlliedUnit.SetActive(false);
        }


        if (unitController.currentlySelectedEnemyUnit() != null && unitController.currentlySelectedEnemyUnit().HitPoints > 0)
        {
            StatusEnemyUnit.transform.FindChild("ATKText").GetComponent<Text>().text = "ATK: " + unitController.currentlySelectedEnemyUnit().AttackFactor;
            StatusEnemyUnit.transform.FindChild("DEFText").GetComponent<Text>().text = "DEF: " + unitController.currentlySelectedEnemyUnit().DefenceFactor;
            StatusEnemyUnit.transform.FindChild("MOVText").GetComponent<Text>().text = "MOV: " + unitController.currentlySelectedEnemyUnit().MovementPoints;
            StatusEnemyUnit.transform.FindChild("SP.ATKText").GetComponent<Text>().text = "SP.ATK: " + (unitController.currentlySelectedEnemyUnit().AttackFactor + unitController.getAdditionalAttackPointsWhenSpecialAttack());
            StatusEnemyUnit.transform.FindChild("ATK.RANText").GetComponent<Text>().text = "ATK.RAN " + unitController.currentlySelectedEnemyUnit().AttackRange;

            StatusEnemyUnit.SetActive(true);
        }
        else
        {
            StatusEnemyUnit.SetActive(false);
        }
    }
}
