using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JanusDirection))]
public class JanusController : MonoBehaviour
{
    public float BlockSize = 32.0f;

    private JanusDirection directionHandler;

    // Start is called before the first frame update
    void Start()
    {
        directionHandler = GetComponent<JanusDirection>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DoStep(JanusColourMode.White);
            
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            DoStep(JanusColourMode.Black);
        }
    }

    public void DoStep(JanusColourMode direction)
    {
        directionHandler.CurrentMode = direction;
        transform.Translate(new Vector3(DirectionFromMode(direction) * BlockSize, 0.0f, 0.0f));
    }

    static float DirectionFromMode(JanusColourMode mode)
    {
        return mode == JanusColourMode.White ? -1.0f : 1.0f;
    }
}
