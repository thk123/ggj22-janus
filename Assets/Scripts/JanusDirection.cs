using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanusDirection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CurrentMode = JanusColourMode.White;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            CurrentMode = JanusColourMode.Black;
        }
    }

    public JanusColourMode CurrentMode
    {
        get;set;
    }
}
