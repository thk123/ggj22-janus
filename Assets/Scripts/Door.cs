using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(GridEntity))]
public class Door : MonoBehaviour
{
    bool isLocked;
    private GridManager gridManager;
    private GridEntity gridEntity;
    // Start is called before the first frame update
    void Start()
    {
        isLocked = true;
        gridManager = GameObject.FindObjectOfType<GridManager>();
        gridEntity = GetComponent<GridEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLocked)
        {
            var overlappingController =
            gridManager.GetCellContents(gridEntity.CurrentPosition)
                .Select(c => c.GetComponent<JanusController>())
                .Where(x => x != null)
                .SingleOrDefault();

            if(overlappingController != null)
            {
                gridManager.CompleteLevel();
                enabled = false;
            }
        }
    }

    public void Unlock()
    {
        isLocked = false;
    }
}
