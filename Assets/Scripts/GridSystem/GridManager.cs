using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2Int GridExtents;
    public string NextLevel;
    private GridEntity[] gridEntities;
    // Start is called before the first frame update
    void Start()
    {
        gridEntities = GameObject.FindObjectsOfType<GridEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetGridCellSize()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width/ Screen.height; 

        float maxCellWidth = width / ((float)GridExtents.x * 2);
        float maxCellHeight = height / ((float)GridExtents.y * 2);

        float cellSize = Mathf.Min(maxCellWidth, maxCellHeight);

        return new Vector2(cellSize, cellSize);
    }

    public bool IsCellInBounds(Vector2Int cell)
    {
        return Mathf.Abs(cell.x) < GridExtents.x
            && Mathf.Abs(cell.y) < GridExtents.y;
    }

    public IEnumerable<GridEntity> GetCellContents(Vector2Int cell)
    {
        return gridEntities.Where(e => e.CurrentPosition == cell);
    }

    public void CompleteLevel()
    {
        if(NextLevel != null)
        {
            SceneManager.LoadScene(NextLevel);
        }
    }
}
