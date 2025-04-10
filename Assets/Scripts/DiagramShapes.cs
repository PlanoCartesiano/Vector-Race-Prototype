using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class DiagramShapes : MonoBehaviour
{
    public static readonly List<Vector2Int> Default = new List<Vector2Int>
    {
        new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(1, 1),
        new Vector2Int(-2, 0), new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0),
        new Vector2Int(-2, -1), new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(1, -1), new Vector2Int(2, -1),
        new Vector2Int(-1, -2), new Vector2Int(0, -2), new Vector2Int(1, -2),
        new Vector2Int(0, -3)
    };

    public static readonly List<Vector2Int> Compact = new List<Vector2Int>
    {
        new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(1, 1),
        new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0),
        new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(1, -1)
    };
}
