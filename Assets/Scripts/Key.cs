using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(GridEntity))]
public class Key : MonoBehaviour
{
    private GridEntity gridEntity;
    private GridManager gridManager;
    private Door doorToUnlock;
    private bool pickedUp;

    public JanusColourMode Colour;
    // Start is called before the first frame update
    void Start()
    {
        gridEntity = GetComponent<GridEntity>();
        gridManager = GameObject.FindObjectOfType<GridManager>();
        doorToUnlock = GameObject.FindObjectsOfType<Door>().Single();
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(pickedUp)
            return;

        var overlappingController =
            gridManager.GetCellContents(gridEntity.CurrentPosition)
                .Select(c => c.GetComponent<JanusDirection>())
                .Where(x => x != null)
                .SingleOrDefault();
        
        if(overlappingController != null && overlappingController.CurrentMode == Colour)
        {
            var keyPosition = doorToUnlock.GetComponent<GridEntity>().CurrentPosition + new Vector2Int(0, 1);
            gridEntity.CurrentPosition = keyPosition;
            doorToUnlock.Unlock();
            pickedUp = true;
        }
    }
}
