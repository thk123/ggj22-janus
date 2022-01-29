using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JanusDirection))]
[RequireComponent(typeof(GridEntity))]
public class JanusController : MonoBehaviour
{
    public float BlockSize = 32.0f;

    private JanusDirection directionHandler;
    private GridEntity gridEntity;
    private GridManager gridManager;


    // Start is called before the first frame update
    void Start()
    {
        directionHandler = GetComponent<JanusDirection>();
        gridEntity = GetComponent<GridEntity>();
        gridManager = GameObject.FindObjectOfType<GridManager>();
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
        gridEntity.Translate(new Vector2Int(DirectionFromMode(direction), 0));
    }

    static int DirectionFromMode(JanusColourMode mode)
    {
        return mode == JanusColourMode.White ? -1 : 1;
    }
}
