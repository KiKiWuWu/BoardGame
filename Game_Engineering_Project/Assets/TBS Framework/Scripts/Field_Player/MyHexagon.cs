using UnityEngine;

class MyHexagon : Hexagon
{
    private Texture textureForTheField;


    /*
    public void Start()
    {
        SetColor(Color.white);
        SetOutlineColor(Color.black);
    }
    */

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
        textureForTheField = Resources.Load("MovementFieldHighlight") as Texture;
        
        SetTexture(textureForTheField);
    }


    public override void MarkAsPath()
    {
        textureForTheField = Resources.Load("MovementPath") as Texture;
        SetTexture(textureForTheField);
    }


    public override void MarkAsHighlighted()
    {
        
    }


    public override void UnMark()
    {
        textureForTheField = Resources.Load("StandardField") as Texture;
        SetTexture(textureForTheField);
        SetOutlineColor(Color.black);
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

