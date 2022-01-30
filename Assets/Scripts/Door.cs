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

    public Sprite UnlockedDoorImage;
    private SpriteRenderer Renderer;
    public float ZoomTime = 0.5f;
    public float EndOrthoSize = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        isLocked = true;
        gridManager = GameObject.FindObjectOfType<GridManager>();
        gridEntity = GetComponent<GridEntity>();
        Renderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLocked)
        {
            var overlappingController =
            gridManager.GetCellContents(gridEntity.StartPosition)
                .Select(c => c.GetComponent<JanusController>())
                .Where(x => x != null)
                .SingleOrDefault();

            if(overlappingController != null)
            {
                StartCoroutine(ZoomInOnDoor());
                
                isLocked = true;
            }
        }
    }

    IEnumerator ZoomInOnDoor()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        foreach(var entityInWorld in GameObject.FindObjectsOfType<GridEntity>())
        {
            entityInWorld.enabled = false;   
        }
        Vector3 cameraStartPos = Camera.main.transform.position;
        float cameraStartSize = Camera.main.orthographicSize;
        var target = GetCentre();
        target.z = cameraStartPos.z;
        float t = 0.0f;
        var curve = AnimationCurve.EaseInOut(0.0f, 0.0f, ZoomTime, 1.0f);
        while(t < ZoomTime)
        {
            var val = curve.Evaluate(t);
            Camera.main.transform.position = Vector3.Lerp(cameraStartPos, target, val);
            Camera.main.orthographicSize = Mathf.Lerp(cameraStartSize, EndOrthoSize, val);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        gridManager.CompleteLevel();

    }

    public void Unlock()
    {
        isLocked = false;
        Renderer.sprite = UnlockedDoorImage;
    }

    public Vector3 GetCentre()
    {
        return Renderer.transform.position;
    }
}
