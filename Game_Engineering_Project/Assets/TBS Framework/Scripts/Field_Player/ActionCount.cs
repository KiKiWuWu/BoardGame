using UnityEngine;
using UnityEngine.UI;

public class ActionCount : MonoBehaviour
{
    public Text RemainingActionPointsOnScreenText;
    public Text CostsForActionOnScreen;

    private static int actionPointsEveryTurn = 17;
    private int currentlyAvailableActionPoints;

    private void Start()
    {
        currentlyAvailableActionPoints = actionPointsEveryTurn;
    }



    private void Update()
    {
        RemainingActionPointsOnScreenText.text = "" + currentlyAvailableActionPoints;
    }



    public void subtractCostOfActionFromCurrentActionCount(int costs)
    {
        currentlyAvailableActionPoints -= costs;
        showCostsOnScreen(costs);
    }



    private void showCostsOnScreen(int cost)
    {
        CostsForActionOnScreen.text = "-" + cost;
        CostsForActionOnScreen.gameObject.SetActive(true);
        Invoke("hideCostDisplayOnScreen", 0.5f);
    }



    private void hideCostDisplayOnScreen()
    {
        CostsForActionOnScreen.gameObject.SetActive(false);
    }



    public void restartAvailableActionPoints()
    {
        currentlyAvailableActionPoints = actionPointsEveryTurn;

        Debug.Log("COUNT IN ACTIONS COUNT SCRIPT = " + currentlyAvailableActionPoints);
    }



    public int remainingActionPoints()
    {
        return currentlyAvailableActionPoints;
    }
}
