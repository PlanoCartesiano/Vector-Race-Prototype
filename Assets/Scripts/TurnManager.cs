using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public CarController player1, player2;
    public CinemachineVirtualCamera virtualCamera;
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
        };

        // Mostra diagrama pro pr�ximo jogador
        Diagram.Instance.ShowDiagram(
            currentPlayer.transform.position,
            currentPlayer.LastMoveVector,
            currentPlayer
        );

        virtualCamera.Follow = Diagram.Instance.diagramCenter.transform;
        //virtualCamera.Follow = currentPlayer.transform;

        Debug.Log($"[TurnManager] � a vez de: {currentPlayer.name}, lastMove: {currentPlayer.LastMoveVector}");
        Debug.Log(currentPlayer.canMove);
    }

    public void CheckForVictory()
    {

        Debug.LogWarning("CheckForVictory chamado");

        if (player1.HasFinished && player2.HasFinished)
        {
            if (player1.Moves < player2.Moves)
                ShowVictory(player1.name);
            else if (player2.Moves < player1.Moves)
                ShowVictory(player2.name);
            else
                ShowVictory("Draw");
        }
    }

private void ShowVictory(string winner)
    {
        VictoryMessage.Instance.ShowVictory($"Fim de jogo! Vencedor: {winner}.");

        Invoke("ResetGame", 7f);

        player1.EnableInput(false);
        player2.EnableInput(false);
    }

private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
