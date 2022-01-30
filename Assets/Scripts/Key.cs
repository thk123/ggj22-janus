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

    public float MoveDuration = 0.5f;

    public float ScaleParam = 0.7f;
    public float ShrinkDuration = 0.5f;

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
            GetComponent<DirectionVisibility>().enabled = false;
            StartCoroutine(SlideToDoor());
            
            pickedUp = true;
        }
    }

    IEnumerator SlideToDoor()
    {
        float t = 0;
        Vector3 startPos = transform.position;
        var curve = AnimationCurve.EaseInOut(0.0f, 0.0f, MoveDuration, 1.0f);
        while(distanceToDoor() > float.Epsilon * float.Epsilon)
        {
            float pos = curve.Evaluate(t);
            transform.position = Vector3.Lerp(startPos, doorToUnlock.GetCentre(), pos);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        t = 0.0f;
        var shrinkCurve = AnimationCurve.EaseInOut(0, 0, ShrinkDuration, 1.0f);
        Vector3 startScale = transform.localScale;
        while(t < ShrinkDuration)
        {
            float pos = shrinkCurve.Evaluate(t);
            transform.localScale = Vector3.Lerp(startScale, ScaleParam * startScale, pos);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        doorToUnlock.Unlock();
    }

    float distanceToDoor() =>(transform.position - doorToUnlock.GetCentre()).sqrMagnitude;

    public bool IsVisible()
    {
        return GetComponent<DirectionVisibility>()?.Visible ?? true;
    }
}
