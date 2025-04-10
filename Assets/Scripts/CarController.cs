using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Vector2 lastMoveVector = Vector2.zero;
    public Vector2 LastMoveVector => lastMoveVector;
    private Diagram diagram;

    [Header("Car Rotation")]
    public float moveSpeed = 5f;
    private Vector2 currentVelocity = Vector2.zero;
    private Vector2 lastPosition;
    private Vector2 targetPosition;
    private bool isMoving = false;

    [Header("LineRenderer Path")]
    private LineRenderer lineRenderer;
    private List<Vector3> pathPoints = new List<Vector3>();

    [Header("Time Counter")]
    private int moves = 0;
    public int Moves => moves;

    [Header("Car Visual")]
    public Transform spriteTransform;

    void Start()
    {

        if (lastMoveVector == Vector2.zero)
            lastMoveVector = Vector2.right;

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

        if (lastMoveVector != Vector2.zero)
        {
            float angle = Mathf.Atan2(lastMoveVector.y, lastMoveVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            //spriteTransform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        transform.position = newPosition;
        moves++;
        diagram.ShowDiagram(newPosition, lastMoveVector);

        AddPointToPath(newPosition);

        Invoke(nameof(ShowDiagramAfterMove), 0.3f);
    }

    private IEnumerator MoveCoroutine()
    {
        isMoving = true;

        float elapsed = 0f;
        Vector2 start = transform.position;

        while (elapsed < 1f)
        {
            transform.position = Vector2.Lerp(start, targetPosition, elapsed);
            elapsed += Time.deltaTime * moveSpeed;
            yield return null;
        }

        transform.position = targetPosition;

        // Rotaciona o carro com base na direção do movimento
        Vector2 direction = targetPosition - lastPosition;
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        isMoving = false;

        // Aqui você chama o Diagram para a próxima jogada, se ainda estiver usando
        FindFirstObjectByType<Diagram>().ShowDiagram(targetPosition, currentVelocity);
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

    public void SetLastMoveVector(Vector2 newMoveVector)
    {
        lastMoveVector = newMoveVector;
    }
}
