using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagramPoint : MonoBehaviour
{
    private Diagram diagram;
    private Vector2 position;
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    private Color hoverColor;

    private int layerMask;

    public void Setup(Diagram diagram, Vector2 position, Color defaultColor, Color hoverColor)
    {
        this.diagram = diagram;
        this.position = position;
        this.defaultColor = defaultColor;
        this.hoverColor = hoverColor;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = defaultColor;

        layerMask = ~LayerMask.GetMask("Ignore Raycast");
    }

    private void OnMouseEnter()
    {
        spriteRenderer.color = hoverColor;
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = defaultColor;
    }

    private void OnMouseDown()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, layerMask);

        if (hit.collider == null || hit.collider.gameObject == gameObject)
        {
            diagram.SelectPoint(position);
        }
    }

    public void OnTouch()
    {
        diagram.SelectPoint(position);
    }
}
