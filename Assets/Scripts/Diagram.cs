using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Diagram;
using static UnityEditor.PlayerSettings;

public class Diagram : MonoBehaviour
{
    public static Diagram Instance;
    public GameObject pointPrefab;
    public Color defaultColor = Color.cyan;
    public Color hoverColor = Color.green;

    private List<GameObject> points = new List<GameObject>();
    private Vector2 lastMoveVector = Vector2.zero;
    private Vector2 currentPosition;

    private CarController currentCar;
    private bool isActive = false;
    private bool isSelecting = false;

    private int diagramSize = 5;
    public enum DiagramShape { Default, Compact};
    private DiagramShape currentShape = DiagramShape.Default;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

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

    public void ShowDiagram(Vector2 carPosition, Vector2 lastMove, CarController carController)
    {
        if(carController.HasFinished)
        {
            TurnManager.Instance.EndTurn();
            return;
        }
        StartCoroutine(ShowDiagramNextFrame(carPosition, lastMove, carController));
    }

    private IEnumerator ShowDiagramNextFrame(Vector2 carPosition, Vector2 lastMove, CarController carController)
    {
        ClearDiagram();

        yield return null;

        currentPosition = carPosition;
        lastMoveVector = lastMove;
        currentCar = carController;

        Vector2 direction = lastMove != Vector2.zero ? lastMove.normalized : Vector2.right;
        transform.right = direction;

        Vector2 center = carPosition + lastMove;
        float angleInDegrees = Vector2.SignedAngle(Vector2.up, direction);

        var shape = carController.InSand
        ? DiagramShape.Compact
        : DiagramShape.Default;

        List<Vector2Int> offsets = shape switch
        {
            DiagramShape.Compact => DiagramShapes.Compact,
            DiagramShape.Default => DiagramShapes.Default,
            _ => DiagramShapes.Default
        };

        /*List<Vector2Int> offsets = currentShape switch
        {
            DiagramShape.Compact => DiagramShapes.Compact,
            DiagramShape.Default => DiagramShapes.Default,
            _ => DiagramShapes.Compact
        };*/

        foreach (Vector2Int offset in offsets)
        {
            Vector2 rotatedOffset = RotateVector(offset, angleInDegrees);
            Vector2 pointPos = center + rotatedOffset;
            CreatePoint(pointPos);
        }

        isActive = true;
    }

    private void CreatePoint(Vector2 position)
    {
        GameObject point = Instantiate(pointPrefab, position, Quaternion.identity, transform);
        points.Add(point);
        point.GetComponent<DiagramPoint>().Setup(this, position, defaultColor, hoverColor);
    }

    public void SetActivePlayer(CarController player)
    {
        currentCar = player;
    }

    public void SelectPoint(Vector2 selectedPosition)
    {
        if (!isActive || currentCar == null || isSelecting)
        {
            Debug.LogWarning("[Diagram] Tentativa de selecionar ponto sem carController ou diagrama inativo.");
            return; 
        }

        isSelecting = true;

        currentCar.MoveTo(selectedPosition);
        isActive = false;
        ClearDiagram();

        Invoke(nameof(ResetSelectionFlag), 0.5f);
    }

    private void ResetSelectionFlag()
    {
        isSelecting = false;
    }

    private void ClearDiagram()
    {

        foreach (GameObject point in points)
        {
            if (point != null)
            {
                point.transform.SetParent(null);
                Destroy(point);
            }
        }
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

    public bool IsValidMove(Vector2 selectedPosition)
    {
        float thresholdDistance = 0.3f;

        foreach (GameObject point in points)
        {
            if (Vector2.Distance(selectedPosition, point.transform.position) <= thresholdDistance)
            {
                return true;
            }
        }

        return false;
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