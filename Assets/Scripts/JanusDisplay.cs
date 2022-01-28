using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JanusDirection))]
[RequireComponent(typeof(SpriteRenderer))]
public class JanusDisplay : MonoBehaviour
{
    public Sprite WhiteSprite;
    public Sprite BlackSprite;

    private JanusDirection directionHandler;
    // Start is called before the first frame update
    void Start()
    {
        directionHandler = GetComponent<JanusDirection>();   
    }

    // Update is called once per frame
    void Update()
    {
        var spriteToUse = directionHandler.CurrentMode == JanusColourMode.White 
                            ? WhiteSprite 
                            : BlackSprite;
        GetComponent<SpriteRenderer>().sprite = spriteToUse;
    }
}
