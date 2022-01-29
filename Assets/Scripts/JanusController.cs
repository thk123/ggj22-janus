using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


class Cooldown
{
    float timeRemaining;
    float cooldownLength;
    KeyCode key;

    public bool IsTriggered
    {
        get; private set;
    }
    public Cooldown(float time, KeyCode key)
    {
        timeRemaining = 0.0f;
        cooldownLength = time;
        this.key = key;
    }

    public void Update()
    {
        IsTriggered = false;
        if(timeRemaining > 0.0f)
        {
            timeRemaining -= Time.deltaTime;
        }
        if(timeRemaining <= 0.0f)
        {
            if(Input.GetKey(key))
            {
                IsTriggered = true;
                timeRemaining = cooldownLength;
            }
        }
    }
}
public class JanusController : MonoBehaviour
{
    public int JumpHeight;
    public float moveTimeDelay = 0.25f;

    private JanusDirection directionHandler;
    private GridEntity gridEntity;
    private GridManager gridManager;

    private Cooldown LeftCooldown;
    private Cooldown RightCooldown;

    private bool currentlyJumping;

    // Start is called before the first frame update
    void Start()
    {
        directionHandler = GetComponent<JanusDirection>();
        gridEntity = GetComponent<GridEntity>();
        gridManager = GameObject.FindObjectOfType<GridManager>();

        LeftCooldown = new Cooldown(moveTimeDelay, KeyCode.LeftArrow);
        RightCooldown = new Cooldown(moveTimeDelay, KeyCode.RightArrow);
        currentlyJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        LeftCooldown.Update();
        RightCooldown.Update();

        var newPos = gridEntity.CurrentPosition;
        JanusColourMode newMode = directionHandler.CurrentMode;
        if(LeftCooldown.IsTriggered && !RightCooldown.IsTriggered)
        {
            newPos = newPos + new Vector2Int(-1, 0);
            newMode = JanusColourMode.White;
        }
        else if(!LeftCooldown.IsTriggered && RightCooldown.IsTriggered)
        {
            newPos = newPos + new Vector2Int(1, 0);
            newMode = JanusColourMode.Black;
        }

        if(CanMoveIntoCell(newPos))
        {
            gridEntity.CurrentPosition = newPos;
            directionHandler.CurrentMode = newMode;
        }

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded())
        {
            StartCoroutine(JumpRoutine());
        }

        if(!currentlyJumping && !IsGrounded())
        {
            StartCoroutine(FallRoutine());
        }
    }

    IEnumerator JumpRoutine()
    {
        currentlyJumping = true;
        int remainingJumpStrength = JumpHeight;
        while(remainingJumpStrength > 0)
        {
            var newPos = gridEntity.CurrentPosition + new Vector2Int(0, 1);
            if(CanMoveIntoCell(newPos))
            {
                gridEntity.CurrentPosition = newPos;
                remainingJumpStrength -= 1;
            }
            else
            {
                remainingJumpStrength = 0;
            }

            yield return new WaitForSeconds(moveTimeDelay);
        }
        currentlyJumping = false;
    }

    IEnumerator FallRoutine()
    {
        currentlyJumping = true;
        while(!IsGrounded())
        {
            var newPos = gridEntity.CurrentPosition + new Vector2Int(0, -1);
            Debug.Assert(CanMoveIntoCell(newPos));
            gridEntity.CurrentPosition = newPos;
            yield return new WaitForSeconds(moveTimeDelay);
        }
        currentlyJumping = false;
    }


    private bool CanMoveIntoCell(Vector2Int targetCell)
    {
        if(!gridManager.IsCellInBounds(targetCell))
        {
            return false;
        }

        var contents = gridManager.GetCellContents(targetCell);

        return contents.All(CanMoveOnto);
    }

    private bool CanMoveOnto(GridEntity contents)
    {
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
        return !(block.GetComponent<DirectionVisibility>()?.Visible ?? true);
    }
}
