using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GridDrawer : MonoBehaviour
{
    public float cellSize = 1f; // Tamanho das células do grid
    public Color gridColor = Color.gray; // Cor da grade

    private List<LineRenderer> lines = new List<LineRenderer>();

    void Start()
    {
        DrawGrid();
    }

    void DrawGrid()
    {
        float camHeight = Camera.main.orthographicSize * 2f;
        float camWidth = camHeight * Camera.main.aspect;

        int gridSizeX = Mathf.CeilToInt(camWidth / cellSize);
        int gridSizeY = Mathf.CeilToInt(camHeight / cellSize);

        float left = -gridSizeX / 2f * cellSize;
        float right = gridSizeX / 2f * cellSize;
        float bottom = -gridSizeY / 2f * cellSize;
        float top = gridSizeY / 2f * cellSize;

        // Criar linhas verticais
        for (int i = 0; i <= gridSizeX; i++)
        {
            float x = left + i * cellSize;
            CreateLine(new Vector3(x, bottom, 0), new Vector3(x, top, 0));
        }

        // Criar linhas horizontais
        for (int i = 0; i <= gridSizeY; i++)
        {
            float y = bottom + i * cellSize;
            CreateLine(new Vector3(left, y, 0), new Vector3(right, y, 0));
        }
    }

    void CreateLine(Vector3 start, Vector3 end)
    {
        GameObject lineObj = new GameObject("GridLine");
        LineRenderer line = lineObj.AddComponent<LineRenderer>();

        line.startColor = gridColor;
        line.endColor = gridColor;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.positionCount = 2;
        line.SetPosition(0, start);
        line.SetPosition(1, end);

        line.material = new Material(Shader.Find("Sprites/Default")); // Shader padrão para visibilidade no 2D

        lines.Add(line);
    }
}
