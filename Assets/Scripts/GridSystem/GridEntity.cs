using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEntity : MonoBehaviour
{
    private GridManager gridManager;
    public Vector2Int CurrentPosition;
    // Start is called before the first frame update
    void Start()
    {
        gridManager = GameObject.FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        var cellSize = gridManager.GetGridCellSize();
        transform.localScale = new Vector3(cellSize.x, cellSize.y, 1.0f);
        transform.localPosition = new Vector3(cellSize.x * CurrentPosition.x, cellSize.y * CurrentPosition.y, 0.0f);
    }

    public void Translate(Vector2Int offset)
    {
        CurrentPosition = CurrentPosition + offset;
    }
}
