using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanusModeResponder : MonoBehaviour
{
    protected JanusDirection DirectionOwner
    {
        get;
        private set;
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        DirectionOwner = GameObject.FindObjectOfType<JanusDirection>() ?? throw new System.NullReferenceException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
