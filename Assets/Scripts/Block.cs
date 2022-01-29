using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum JanusColourMode
{
    White,
    Black
}

[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{
    public JanusColourMode Colour;

    private JanusDirection janus;

    // Start is called before the first frame update
    void Start()
    {
        janus = FindObjectsOfType<JanusDirection>().First();
    }

    // Update is called once per frame
    void Update()
    {
        if(Visible)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public bool Visible => janus.CurrentMode == Colour;

    private void Show()
    {
        GetComponent<Renderer>().enabled = true;
    }

    private void Hide()
    {
        GetComponent<Renderer>().enabled = false;
    }
}
