using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VectorCar : MonoBehaviour
{
    public Vector2 position;
    public Vector2 velocity;
    public Vector2 acceleration;
    public float gridSize = 1f;
    public TextMeshProUGUI infoText;

    private void Start()
    {
        position = new Vector2(0, 0);
        velocity = Vector2.zero;
        acceleration = Vector2.zero;
        UpdatePosition();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetPosition();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) acceleration += Vector2.up;
        if (Input.GetKeyDown(KeyCode.DownArrow)) acceleration += Vector2.down;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) acceleration += Vector2.left;
        if (Input.GetKeyDown(KeyCode.RightArrow)) acceleration += Vector2.right;

        if (Input.GetKeyDown(KeyCode.Space)) ApplyMovement();
    }

    void ApplyMovement()
    {
        velocity += acceleration;
        position += velocity;
        acceleration = Vector2.zero;
        UpdatePosition();
    }

    void ResetPosition()
    {
        transform.position = new Vector3(0, 0, 0);
        position = new Vector2(0, 0);
        velocity = Vector2.zero;
        acceleration = Vector2.zero;
    }

    void UpdatePosition()
    {
        transform.position = new Vector3(position.x * gridSize, position.y * gridSize, 0);
        if (infoText != null)
        {
            infoText.text = $"Pos: {position}\nVel: {velocity}\nAcc: {acceleration}";
        }
    }
}
