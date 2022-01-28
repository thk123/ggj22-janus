using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : JanusModeResponder
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Camera>().backgroundColor = GetBackgroundColourFromJanusMode(DirectionOwner.CurrentMode);
    }

    private Color GetBackgroundColourFromJanusMode(JanusColourMode mode)
    {
        return mode == JanusColourMode.White 
            ? Color.black
            : Color.white;
    }
}
