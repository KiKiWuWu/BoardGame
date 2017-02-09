using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleUnit : Unit
{
    Coroutine PulseCoroutine;


    public Color LeadingColor;
    public override void Initialize()
    {
        base.Initialize();
        transform.position += new Vector3(0, 0, -1);
        GetComponent<Renderer>().material.color = LeadingColor;
    }

    public override void OnUnitDeselected()
    {
        base.OnUnitDeselected();
        StopCoroutine(PulseCoroutine);
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    public override void MarkAsAttacking(Unit other)
    {
        StartCoroutine(Jerk(other, 0.15f));
    }

    public override void MarkAsDefending(Unit other)
    {
        StartCoroutine(Jerk(other, 0.15f));
    }

    public override void MarkAsDestroyed()
    {      
    }

    public override void MarkAsFinished()
    {
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



    private IEnumerator Pulse(float breakTime, float delay, float scaleFactor)
    {
        var baseScale = transform.localScale;
        while (true)
        {
            float time1 = Time.time;
            while (time1 + delay > Time.time)
            {
                transform.localScale = Vector3.Lerp(baseScale * scaleFactor, baseScale, (time1 + delay) - Time.time);
                yield return 0;
            }

            float time2 = Time.time;
            while (time2 + delay > Time.time)
            {
                transform.localScale = Vector3.Lerp(baseScale, baseScale * scaleFactor, (time2 + delay) - Time.time);
                yield return 0;
            }

            yield return new WaitForSeconds(breakTime);
        }
    }


    //Highlights all allies on the field
    public override void MarkAsFriendly()
    {
        //GetComponent<Renderer>().material.color = LeadingColor + new Color(0.8f, 1, 0.8f);
    }

    public override void MarkAsReachableEnemy()
    {
        SetHighlighter("enemy");

        //GetComponent<Renderer>().material.mainTexture = Resources.Load("AttackField") as Texture;
        //GetComponent<Renderer>().material.color = LeadingColor + Color.red;
    }

    public override void MarkAsSelected()
    {
        PulseCoroutine = StartCoroutine(Pulse(0.7f, 0.5f, 1.2f));

        //GetComponent<Renderer>().material.color = LeadingColor + Color.green;
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
        var highlighter = transform.Find("HighlighterField").GetComponent<Renderer>();
        if (highlighter != null)
        {
            highlighter.gameObject.SetActive(true);

            if (command == "enemy")
            {
                highlighter.material.mainTexture = Resources.Load("AttackField") as Texture;
            }

            if(command == "unmark")
            {
                highlighter.gameObject.SetActive(false);
            }
        }        
    }
}
