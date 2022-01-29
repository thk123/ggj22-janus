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

    private bool flightInMotion;


    // Start is called before the first frame update
    void Start()
    {
        directionHandler = GetComponent<JanusDirection>();
        gridEntity = GetComponent<GridEntity>();
        gridManager = GameObject.FindObjectOfType<GridManager>();
        flightInMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!flightInMotion)
        {
            UpdateFalling();
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                DoStep(JanusColourMode.White);
                
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                DoStep(JanusColourMode.Black);
            }
        }
    }

    private void UpdateFalling()
    {
        var targetCell = gridEntity.CurrentPosition + new Vector2Int(0, -1);
        if(CanMoveIntoCell(targetCell))
        {
            StartCoroutine(FallingRoutine(targetCell));
            flightInMotion = true;
        }
    }

    IEnumerator FallingRoutine(Vector2Int fallTo)
    {
        gridEntity.CurrentPosition = fallTo;
        yield return new WaitForSeconds(0.5f);
        flightInMotion = false;
    }

    public void DoStep(JanusColourMode direction)
    {
        var moveDirection = new Vector2Int(DirectionFromMode(direction), 0);
        var targetCell = gridEntity.CurrentPosition + moveDirection;
        if(CanMoveIntoCell(targetCell))
        {
            directionHandler.CurrentMode = direction;
            gridEntity.CurrentPosition = targetCell;
        }
    }

    private bool CanMoveIntoCell(Vector2Int targetCell)
    {
        if(!gridManager.IsCellInBounds(targetCell))
        {
            return false;
        }

        var contents = gridManager.GetCellContents(targetCell);

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
