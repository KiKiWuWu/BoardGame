using UnityEngine;

class MyHexagon : Hexagon
{
    private Transform HighlightField;

    
    public void Start()
    {
        HighlightField = transform.FindChild("HexagonHighlighter");
    }
    

    public override Vector3 GetCellDimensions()
    {
        var center = Vector3.zero;

        foreach (Transform child in transform.FindChild("Outline"))
        {
            if (child.GetComponent<Renderer>() == null)
                continue;
            center += child.GetComponent<Renderer>().bounds.center;
        }
        center /= transform.FindChild("Outline").childCount;

        Bounds ret = new Bounds(center, Vector3.zero);
        foreach (Transform child in transform.FindChild("Outline"))
        {
            if (child.GetComponent<Renderer>() == null)
                continue;
            ret.Encapsulate(child.GetComponent<Renderer>().bounds);
        }
        return ret.size;
    }

    public override void MarkAsReachable()
    {
        setColorOfHighlighter("reachable");
    }


    public override void MarkAsPath()
    {
        setColorOfHighlighter("path");
    }


    public override void MarkAsHighlighted()
    {
        
    }


    public override void UnMark()
    {
        setColorOfHighlighter("unmark");
    }



    private void setColorOfHighlighter(string command)
    {
        if (transform.FindChild("HexagonHighlighter") != null)
        {
            var highLightField = transform.FindChild("HexagonHighlighter").GetComponent<Renderer>();


            if (command == "reachable")
            {
                highLightField.material.color = new Color(1f, 0.92f, 0.016f, 0.5f);
                transform.FindChild("HexagonHighlighter").gameObject.SetActive(true);
            }

            if (command == "path")
            {
                highLightField.material.color = new Color(0, 1, 1, 0.5f);
            }

            if (command == "unmark")
            {
                transform.FindChild("HexagonHighlighter").gameObject.SetActive(false);
            }
        }
    }



    private void SetTexture(Texture floorTexture)
    {

        var rendererComponent = transform.GetChild(0).GetComponent<Renderer>();
        rendererComponent.material.mainTexture = floorTexture;
    }


    private void SetOutlineColor(Color color)
    {
        var outline = transform.FindChild("Outline");
        for (int i = 0; i < outline.transform.childCount; i++)
        {
            var rendererComponent = outline.transform.GetChild(i).GetComponent<Renderer>();
            if (rendererComponent != null)
                rendererComponent.material.color = color;
        }
    } 
}

