using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2Int GridSize;
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

        return new Vector2(width / (float)GridSize.x, height / (float)GridSize.y);
    }

    
}
