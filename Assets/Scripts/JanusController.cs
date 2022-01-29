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
        var moveDirection = new Vector2Int(DirectionFromMode(direction), 0);
        var targetCell = gridEntity.CurrentPosition + moveDirection;
        if(gridManager.IsCellInBounds(targetCell))
        {
            if(CanMoveIntoCell(gridManager.GetCellContents(targetCell)))
            {
                directionHandler.CurrentMode = direction;
                gridEntity.CurrentPosition = targetCell;
            }
        }
    }

    private bool CanMoveIntoCell(GridEntity contents)
    {
        if(contents == null)
            return true;

        return CanMoveInToBlockCell(contents.GetComponent<Block>());
    }

    private bool CanMoveInToBlockCell(Block block)
    {
        if(block == null)
            return true;
        return !block.Visible;
    }

    static int DirectionFromMode(JanusColourMode mode)
    {
        return mode == JanusColourMode.White ? -1 : 1;
    }
}
