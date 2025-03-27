using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ShowDiagram(Vector2 carPosition, Vector2 lastMove)
    {
        ClearDiagram(); // Limpa pontos antigos
        currentPosition = carPosition;
        lastMoveVector = lastMove;

        Vector2 center = carPosition + lastMove; // Centro do GG

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2 pointPos = center + new Vector2(x, y);
                CreatePoint(pointPos);
            }
        }

        isActive = true;
    }

    private void CreatePoint(Vector2 position)
    {
        Debug.Log($"Criando ponto no grid em: {position}");
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
}
