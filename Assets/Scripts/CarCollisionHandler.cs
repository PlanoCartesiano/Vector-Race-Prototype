using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisionHandler : MonoBehaviour
{
    public Diagram diagram;
    public CarController car;
    public float delayBeforeReset = 1.5f;

    private bool penalizing = false;

    private void Awake()
    {
        car = GetComponent<CarController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se a colis�o foi com a borda da pista (tag "TrackBorder")
        if (collision.CompareTag("TrackBorder") && !penalizing)
        {
            penalizing = true;
            StartCoroutine(HandleCollision());
        }
    }

    private IEnumerator HandleCollision()
    {
        Debug.Log("Voc� colidiu ou saiu da pista! Reiniciando a jogada com velocidade zero.");
        yield return new WaitForSeconds(delayBeforeReset);

        diagram.ResetLastMoveVector();
        car.SetLastMoveVector(car.transform.right);
        TurnManager.Instance.EndTurn();

        penalizing = false;
    }
}
