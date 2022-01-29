using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(JanusDirection))]
[RequireComponent(typeof(GridEntity))]
public class JanusController : MonoBehaviour
{
    private JanusDirection directionHandler;
    private GridEntity gridEntity;
    private GridManager gridManager;


    Vector2Int movementForce;
    Vector2Int lastAppliedForce;


    public int JumpHeight = 2;


    // Start is called before the first frame update
    void Start()
    {
        directionHandler = GetComponent<JanusDirection>();
        gridEntity = GetComponent<GridEntity>();
        gridManager = GameObject.FindObjectOfType<GridManager>();
        movementForce = Vector2Int.zero;
        lastAppliedForce = Vector2Int.zero;
        StartCoroutine(MoveLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            movementForce = new Vector2Int(movementForce.x, JumpHeight);
        }

        if(!IsGrounded() && movementForce.y == 0)
        {
            bool isGrounded = IsGrounded();
            movementForce = new Vector2Int(movementForce.x, -1);
        }
        
        bool leftPressed = Input.GetKey(KeyCode.LeftArrow);
        bool rightPressed = Input.GetKey(KeyCode.RightArrow);
        if(leftPressed && !rightPressed)
        {
            movementForce = new Vector2Int(-1, movementForce.y);
        }
        else if(!leftPressed && rightPressed)
        {
            movementForce = new Vector2Int(1, movementForce.y);
        }
        else if(lastAppliedForce.x != 0)
        {
            movementForce = new Vector2Int(0, movementForce.y);
        }
    }

    IEnumerator MoveLoop()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.25f);
            DoOneStep();
        }
    }

    void DoOneStep()
    {
        var moveDirection = DirectionFromForce(movementForce);
        var newTarget = gridEntity.CurrentPosition + moveDirection;
        if(CanMoveIntoCell(newTarget))
        {
            gridEntity.CurrentPosition = newTarget;

            if(moveDirection.x > 0)
            {
                directionHandler.CurrentMode = JanusColourMode.Black;   
            }
            else if(moveDirection.x < 0)
            {
                directionHandler.CurrentMode = JanusColourMode.White;   
            }
        }
        lastAppliedForce = moveDirection;
        movementForce = DecreaseMovementByOne(movementForce);
    }

    private static Vector2Int DirectionFromForce(Vector2Int movementForce)
    {
        return new Vector2Int(UnitLength(movementForce.x), UnitLength(movementForce.y));
    }

    private static int UnitLength(int val)
    {
        if(val > 0)
            return 1;
        else if (val < 0)
            return -1;
        else
            return 0;
    }

    private static Vector2Int DecreaseMovementByOne(Vector2Int movementForce)
    {
        return new Vector2Int(TendTowardZero(movementForce.x), TendTowardZero(movementForce.y));
    }

    private static int TendTowardZero(int value)
    {
        if(value > 0)
            return value - 1;
        else if(value < 0)
            return value + 1;
        return 0;
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

    private bool IsGrounded()
    {
        var groundCell = gridEntity.CurrentPosition + new Vector2Int(0, -1);
        return !CanMoveIntoCell(groundCell);
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
