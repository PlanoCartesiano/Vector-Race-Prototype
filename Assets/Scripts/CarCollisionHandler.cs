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
        // Verifica se a colisão foi com a borda da pista (tag "TrackBorder")
        if (collision.CompareTag("TrackBorder") && !penalizing)
        {
            penalizing = true;
            StartCoroutine(HandleCollision());
        }
    }

    private IEnumerator HandleCollision()
    {
        Debug.Log("Você colidiu ou saiu da pista! Reiniciando a jogada com velocidade zero.");
        yield return new WaitForSeconds(delayBeforeReset);

        // Mantém o carro na mesma posição (pois ele saiu da pista) e zera o vetor de movimento.
        diagram.ResetLastMoveVector();            // Zera o vetor (o diagrama passará a ser gerado com vetor zero)
        car.SetLastMoveVector(car.transform.right);
        diagram.ShowDiagram(transform.position, Vector2.zero); // Atualiza o diagrama no mesmo ponto com velocidade 0

        penalizing = false;
    }
}
