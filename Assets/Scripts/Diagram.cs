using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Diagram;

public class Diagram : MonoBehaviour
{
    public GameObject pointPrefab;
    public Color defaultColor = Color.cyan;
    public Color hoverColor = Color.green;

    private List<GameObject> points = new List<GameObject>();
    private Vector2 lastMoveVector = Vector2.zero;
    private Vector2 currentPosition;
    private bool isActive = false;

    private CarController carController;
    private int diagramSize = 5;

    public enum DiagramShape { Default, Compact};
    private DiagramShape currentShape = DiagramShape.Default;

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

            if (hit.collider != null && hit.collider.GetComponent<DiagramPoint>() != null)
            {
                hit.collider.GetComponent<DiagramPoint>().OnTouch();
            }
        }
    }

    public void SetShape(DiagramShape shape)
    {
        currentShape = shape;
    }

    public void ShowDiagram(Vector2 carPosition, Vector2 lastMove)
    {
        ClearDiagram();
        currentPosition = carPosition;
        lastMoveVector = lastMove;

        /*if (lastMove != Vector2.zero)
            transform.right = lastMove.normalized;*/ // mudança x

        Vector2 direction = lastMove != Vector2.zero ? lastMove.normalized : Vector2.right;

        transform.right = direction;

        Vector2 center = carPosition + lastMove;

        Debug.DrawRay(center, direction, Color.red, 2f);

        //Vector2 center = carPosition + lastMove; // + lastMove mudança x (retorne todos os x para voltar a funcionar igualmente).

        List<Vector2Int> offsets = currentShape switch
        {
            DiagramShape.Compact => DiagramShapes.Compact,
            DiagramShape.Default => DiagramShapes.Default,_ => DiagramShapes.Compact
        };

        float angleInDegrees = Vector2.SignedAngle(Vector2.up, direction);

        //float angle = Mathf.Atan2(direction.y, direction.x);
        //float angleInDegrees = angle * Mathf.Rad2Deg; // lastMove torna-se direction

        foreach (Vector2Int offset in offsets)
        {
            Vector2 rotatedOffset = RotateVector(offset, angleInDegrees);
            Vector2 pointPos = center + rotatedOffset; // <- voltou ao normal, sem arredondar
            CreatePoint(pointPos);
            //Vector2 rawPointPos = center + rotatedOffset;
            // Vector2 alignedPointPos = new Vector2(Mathf.Round(rawPointPos.x), Mathf.Round(rawPointPos.y));
            //Vector2 pointPos = center + RotateVector(offset, angleInDegrees); /* + new Vector2(offset.x, offset.y);*/
            //CreatePoint(alignedPointPos);
        };

        /*int halfSize = diagramSize / 2;

        for (int x = -halfSize; x <= halfSize; x++)
        {
            /*for (int y = -halfSize; y <= halfSize; y++)
            {
                Vector2 pointPos = center + new Vector2(x, y);
                CreatePoint(pointPos);
            }*/

        /*for (int y = -halfSize; y <= halfSize; y++)
        {
            // Pula os cantos (ex: -2,-2), (-2,2), (2,-2), (2,2)
            if (Mathf.Abs(x) == 2 && Mathf.Abs(y) == 2)
                continue;

            Vector2 pointPos = center + new Vector2(x, y);
            CreatePoint(pointPos);
        }
    }*/

        isActive = true;
    }

    private void CreatePoint(Vector2 position)
    {
        //Debug.Log($"Criando ponto no grid em: {position}");
        GameObject point = Instantiate(pointPrefab, position, Quaternion.identity, transform);
        points.Add(point);
        point.GetComponent<DiagramPoint>().Setup(this, position, defaultColor, hoverColor);
    }

    public void SelectPoint(Vector2 selectedPosition)
    {
        if (!isActive) return;

        carController = FindFirstObjectByType(typeof(CarController)) as CarController;
        carController.MoveTo(selectedPosition);

        isActive = false;
        ClearDiagram();
    }

    private void ClearDiagram()
    {
        foreach (GameObject point in points)
            Destroy(point);
        points.Clear();
    }

    private Vector2 RotateVector(Vector2 original, float angleDegrees)
    {
        float rad = angleDegrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(
            original.x * cos - original.y * sin,
            original.x * sin + original.y * cos
        );
    }

    public void SetDiagramSize(int newSize)
    {
        diagramSize = newSize;
    }

    public void ResetLastMoveVector()
    {
        lastMoveVector = Vector2.zero;
    }
}
