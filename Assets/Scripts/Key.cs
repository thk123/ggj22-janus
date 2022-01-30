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
            gridManager.GetCellContents(gridEntity.StartPosition)
                .Select(c => c.GetComponent<JanusDirection>())
                .Where(x => x != null)
                .SingleOrDefault();
        
        if(overlappingController != null && IsVisible())
        {
            //var keyPosition = doorToUnlock.GetComponent<GridEntity>().StartPosition + new Vector2Int(0, 1);
            //gridEntity.StartPosition = keyPosition;
            gridEntity.enabled = false;
            StartCoroutine(SlideToDoor());
            
            pickedUp = true;
        }
    }

    IEnumerator SlideToDoor()
    {
        float t = 0;
        Vector3 startPos = transform.position;
        var curve = AnimationCurve.EaseInOut(0, 0, 1.0f, 1.0f);
        while(distanceToDoor() > float.Epsilon * float.Epsilon)
        {
            float pos = curve.Evaluate(t);
            transform.position = Vector3.Lerp(startPos, doorToUnlock.transform.GetChild(0).position, pos);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        doorToUnlock.Unlock();
    }

    float distanceToDoor() =>(transform.position - doorToUnlock.transform.position).sqrMagnitude;

    public bool IsVisible()
    {
        return GetComponent<DirectionVisibility>()?.Visible ?? true;
    }
}
