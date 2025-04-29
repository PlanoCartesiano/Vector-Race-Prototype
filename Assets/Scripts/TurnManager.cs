using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public CarController player1, player2;
    private CarController currentPlayer;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentPlayer = player1;
        player1.EnableInput(true);
        player2.EnableInput(false);

        // Diagrama inicial
        Diagram.Instance.ShowDiagram(
            currentPlayer.transform.position,
            currentPlayer.LastMoveVector,
            currentPlayer
        );
    }

    public void EndTurn()
    {
        // Alterna quem joga
        if (currentPlayer == player1)
        {
            player1.EnableInput(false);
            player2.EnableInput(true);
            currentPlayer = player2;
        }
        else
        {
            player2.EnableInput(false);
            player1.EnableInput(true);
            currentPlayer = player1;
        }

        // Mostra diagrama pro próximo jogador
        Diagram.Instance.ShowDiagram(
            currentPlayer.transform.position,
            currentPlayer.LastMoveVector,
            currentPlayer
        );

        Debug.Log($"[TurnManager] É a vez de: {currentPlayer.name}, lastMove: {currentPlayer.LastMoveVector}");
        Debug.Log(currentPlayer.canMove);
    }
}
