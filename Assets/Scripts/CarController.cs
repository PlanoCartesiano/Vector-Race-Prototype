using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Vector2 lastMoveVector = Vector2.zero;
    private Diagram diagram;

    [Header("LineRenderer Path")]
    private LineRenderer lineRenderer;
    private List<Vector3> pathPoints = new List<Vector3>();

    [Header("Time Counter")]
    private int moves = 0;
    public int Moves => moves;

    void Start()
    {
        diagram = FindFirstObjectByType(typeof(Diagram)) as Diagram;
        diagram.ShowDiagram(transform.position, lastMoveVector);

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 0;

        AddPointToPath(transform.position);
    }

    public void MoveTo(Vector2 newPosition)
    {
        lastMoveVector = newPosition - (Vector2)transform.position;
        transform.position = newPosition;
        moves++;
        diagram.ShowDiagram(newPosition, lastMoveVector);

        AddPointToPath(newPosition);

        Invoke(nameof(ShowDiagramAfterMove), 0.3f);
    }

    public void ShowDiagramAfterMove()
    {
        diagram.ShowDiagram((Vector2)transform.position, lastMoveVector);
    }

    private void AddPointToPath(Vector2 newPoint)
    {
        pathPoints.Add(newPoint);
        lineRenderer.positionCount = pathPoints.Count;
        lineRenderer.SetPositions(pathPoints.ToArray());
    }
}
