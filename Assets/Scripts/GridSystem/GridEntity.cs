using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEntity : MonoBehaviour
{
    private GridManager gridManager;
    public Vector2Int StartGridPos;
    // Start is called before the first frame update
    void Start()
    {
        CurrentPos = StartGridPos;   
        gridManager = GameObject.FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        var cellSize = gridManager.GetGridCellSize();
        transform.localScale = new Vector3(cellSize.x, cellSize.y, 1.0f);
        transform.position = new Vector3(cellSize.x * StartGridPos.x, cellSize.y * StartGridPos.y, 0.0f);
    }

    public Vector2Int CurrentPos
    {
        get; private set;
    }
}
