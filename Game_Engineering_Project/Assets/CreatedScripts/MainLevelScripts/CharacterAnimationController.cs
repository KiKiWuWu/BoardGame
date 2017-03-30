using System.Collections;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{

    //Starts the walking animation of the current walking unit
    public void startWalkingAnimation(Unit unit)
    {
        unit.transform.FindChild("" + unit.name + "Character").GetComponent<Animation>().Play("WalkingAnimation");
    }


    //Stops the walking animation and displays a idle animation of the current selected unit
    public void endWalkingAnimation(Unit unit)
    {
        unit.transform.FindChild("" + unit.name + "Character").GetComponent<Animation>().Stop("WalkingAnimation");

        unit.transform.FindChild("" + unit.name + "Character").GetComponent<Animation>().Play("IdleAnimation");
    }


    //Shows a animation in which the unit takes a defensiv position
    public void showCharacterTakingDefencePositionAnimation(Unit unit)
    {
        unit.transform.FindChild("" + unit.name + "Character").GetComponent<Animation>().Play("DefenceOnAnimation");
    }


    //Shows a animation in which the selected unit leaves the defensiv position
    public void showCharacterLeavingDefencePositionOffAnimation(Unit unit)
    {
        unit.transform.FindChild("" + unit.name + "Character").GetComponent<Animation>().Play("DefenceOffAnimation");
    }


    //Shows a dying animation if a unit reached zero health points
    public void showCharacterDyingAnimation(Unit unit)
    {
        unit.transform.FindChild("" + unit.name + "Character").GetComponent<Animation>().Play("DyingAnimation");
    }


    //Shows the attack animation of the currently attacking unit
    public void showCharacterAttackAnimation(Unit unit)
    {
        unit.transform.FindChild("" + unit.name + "Character").GetComponent<Animation>().Play("AttackAnimation");
        StartCoroutine(showCharactersIdleAnimation(unit));
    }


    //Shows the idle animation of a unit
    private IEnumerator showCharactersIdleAnimation(Unit unit)
    {
        yield return new WaitForSeconds(2f);

        unit.transform.FindChild("" + unit.name + "Character").GetComponent<Animation>().Play("IdleAnimation");

        yield return null;
    }
}