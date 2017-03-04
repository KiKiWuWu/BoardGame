using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SampleUnit : Unit
{
    //Coroutine PulseCoroutine;

    private CursorOverPlayerController cursorController;
    private GUIControllerHexa gUIController;
    private Player player;


    public Color LeadingColor;
    public override void Initialize()
    {
        base.Initialize();
        player = gameObject.GetComponent<Player>();
        transform.position += new Vector3(0, 0, -1);
        GetComponent<Renderer>().material.color = LeadingColor;
        cursorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<CursorOverPlayerController>();
        gUIController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GUIControllerHexa>();
    }

    public override void OnUnitDeselected()
    {
        base.OnUnitDeselected();

        //StopCoroutine(PulseCoroutine);

        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //StartCoroutine(fade(2));
        //gUIController.UITopLeft.SetActive(false);
        
        cursorController.hidePlayerCursor();


        //gUIController.hidePlayerUI();
    }

    public override void MarkAsAttacking(Unit other)
    {

        StartCoroutine(Jerk(other, 0.15f));
    }

    public override void MarkAsDefending(Unit other)
    {
     
        //gUIController.UITopRight.SetActive(true);
        StartCoroutine(Jerk(other, 0.15f));
        gUIController.UITopRight.SetActive(true);
        //HitPoints = HitPoints - AttackFactor;
        print(HitPoints + " HP" + TotalHitPoints + " THP");
        float HPproc1 = ((float)(HitPoints) / TotalHitPoints) * 100;
        print("%: "+HPproc1);
        gUIController.HPSliderEnemy.value = (float)HPproc1;
        gUIController.HPEnemy.text = "" + (HitPoints - AttackFactor) + "/" + TotalHitPoints + " HP";

        //gUIController.UITopRight.SetActive(false);
    }

    public override void MarkAsDestroyed()
    {      
        
    }

    public override void MarkAsFinished()
    {
      
    }

    /*private IEnumerator fade(float waittime)
    {
        float StartTime = Time.time;
        
        while (true)
        {
            var currentTime = Time.time;
            if (StartTime + waittime < currentTime)
                break;

            yield return new WaitForSeconds(3);
            print("Fade");
        }
    }*/

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


    /*
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
    */


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
        gUIController.UITopLeft.SetActive(true);
        cursorController.showCursorOverCurrentPlayer(this);
       /* if (PlayerNumber == 0)
        {
            print("Player: " + PlayerNumber);
            //gUIController.ImagePlayer.color = new Color32 (255,255,0,255);
             
        }
        else
        {
            //gUIController.ImagePlayer.color = Color.red;
        };
        */
        float HPproc = ((float)HitPoints/ TotalHitPoints)* 100;
        print("HPProc:" + HPproc);
        
      /*  if (HPproc < 90)
        {
            gUIController.HPSliderPlayer
        }*/

        gUIController.HPSliderPlayer.value = (float) HPproc; 
        gUIController.HPPlayer.text = "" + HitPoints+"/"+TotalHitPoints + " HP";
        //PulseCoroutine = StartCoroutine(Pulse(0.7f, 0.5f, 1.2f));


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
