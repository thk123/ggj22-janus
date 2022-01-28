using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BorderController : JanusModeResponder
{
    private Image image;
    


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        image  = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.color = GetBorderColourFromJanusMode(DirectionOwner.CurrentMode);
    }

    private Color GetBorderColourFromJanusMode(JanusColourMode mode)
    {
        return mode == JanusColourMode.White 
            ? Color.white
            : Color.black;
    }
}
