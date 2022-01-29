using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    public int JumpHeight = 2;

    public bool IsJumping => jumpPoints.Any();

    private List<Vector2Int> jumpPoints; 

    // Start is called before the first frame update
    void Start()
    {
        jumpPoints = new List<Vector2Int>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsJumping)
        {

        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DoJump();  
        }
    }

    public void DoJump()
    {
        var jumpPoints = Enumerable
            .Range(1, JumpHeight)
            // .Select(height => gridEntity.CurrentPosition + new Vector2Int(0, height))
            //.Concat(lastPoint.HasValue ? new Vector2Int[]{lastPoint.Value} : Enumerable.Empty<Vector2Int>())
            .ToList();
    }
}
