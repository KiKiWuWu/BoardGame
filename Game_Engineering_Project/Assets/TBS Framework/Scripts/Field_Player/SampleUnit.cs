using System.Collections;
using UnityEngine;

public class SampleUnit : Unit
{
    private GUIControllerHexa GUIController;
    private CharacterSpecialAttackController specialAttackContr;
    private AllUnitsController unitController;

    public Color LeadingColor;
    public bool temp = false;



    public override void Initialize()
    {
        base.Initialize();
        transform.position += new Vector3(0, 0, -1);
        GetComponent<Renderer>().material.color = LeadingColor;

        GUIController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GUIControllerHexa>();
        specialAttackContr = GameObject.FindGameObjectWithTag("GameController").GetComponent<CharacterSpecialAttackController>();
        unitController = GameObject.FindGameObjectWithTag("GameController").GetComponent<AllUnitsController>();
    }

    public override void OnUnitDeselected()
    {
        if (unitController.isAttackCurrentlyPerformed())
        {
            unitController.stateOnAttack(false);
        }
        else
        {
            unitController.selectedAlliedUnitByPlayer(null);
            GUIController.hideHPBarOnScreen("friend");
        }

        base.OnUnitDeselected();
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
             
        GUIController.hideSpecialAttackButtonArea();
        specialAttackContr.setSpecialAttackButtonToDefault();
    }

    public override void MarkAsAttacking(Unit other)
    {
        unitController.stateOnAttack(true);
        StartCoroutine(Jerk(other, 0.15f));
    }

    public override void MarkAsDefending(Unit other)
    {
        StartCoroutine(Jerk(other, 0.15f));
    }

    public override void MarkAsDestroyed()
    {
        unitController.selectedEnemyUnitByPlayer(null);
    }


    public override void MarkAsFinished()
    {
        //specialAttackContr.setSpecialAttackButtonToDefault();
    }


    private IEnumerator Jerk(Unit other, float movementTime)
    {
        var heading = other.transform.position - transform.position;
        var direction = heading / heading.magnitude;
        float startTime = Time.time;

        while (true)
        {
            var currentTime = Time.time;
            if (startTime + movementTime < currentTime)
                break;
            transform.position = Vector3.Lerp(transform.position, transform.position + (direction / 2.5f), ((startTime + movementTime) - currentTime));
            yield return 0;
        }
        startTime = Time.time;
        while (true)
        {
            var currentTime = Time.time;
            if (startTime + movementTime < currentTime)
                break;
            transform.position = Vector3.Lerp(transform.position, transform.position - (direction / 2.5f), ((startTime + movementTime) - currentTime));
            yield return 0;
        }
        transform.position = Cell.transform.position + new Vector3(0, 0, -1);
    }



    //Highlights all allies on the field
    public override void MarkAsFriendly()
    {
        
    }


    public override void MarkAsReachableEnemy()
    {
        SetHighlighter("enemy");

        //GetComponent<Renderer>().material.mainTexture = Resources.Load("AttackField") as Texture;
        //GetComponent<Renderer>().material.color = LeadingColor + Color.red;
    }


    public override void MarkAsSelected()
    {
        unitController.selectedAlliedUnitByPlayer(this);
        GUIController.showHPBarOfSelectedUnit("friend", HitPoints, TotalHitPoints);
        
        GUIController.showSpecialAttackButtonArea();
    }

    //Removes highlighter from the attackable Units
    public override void UnMark()
    {
        //GetComponent<Renderer>().material.mainTexture = Resources.Load("StandardField") as Texture;
        //GetComponent<Renderer>().material.color = LeadingColor;
        SetHighlighter("unmark");
    }


    private void SetHighlighter(string command)
    {
        var CharacterHighlighter = transform.FindChild("CharacterHighlighter");
        CharacterHighlighter.gameObject.SetActive(true);

        if (command == "enemy")
        {
            CharacterHighlighter.GetComponent<Renderer>().material.color = new Color(1f, 0, 0, 0.8f);
        }

        if (command == "unmark")
        {
            CharacterHighlighter.gameObject.SetActive(false);
        }        
    }
}
