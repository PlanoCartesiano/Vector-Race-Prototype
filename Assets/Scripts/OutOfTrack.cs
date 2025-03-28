using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfTrack : MonoBehaviour
{
    private Diagram diagram;
    private int sandTileCount = 0;

    void Start()
    {
        diagram = FindFirstObjectByType(typeof(Diagram)) as Diagram;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sand")
        {
            sandTileCount++;
            diagram.SetDiagramSize(3);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sand")
        {
            sandTileCount = Mathf.Max(sandTileCount - 1, 0);

            if (sandTileCount == 0)
            {
                diagram.SetDiagramSize(5);
            }
        }
    }
}
