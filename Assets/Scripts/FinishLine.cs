using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishLine : MonoBehaviour
{
    public GameObject victorySpam;

    private void Start()
    {
        if (victorySpam != null)
            victorySpam.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🏁 Vitória! O carro cruzou a linha de chegada!");

            if (victorySpam != null)
                victorySpam.SetActive(true);

            Invoke("ResetGame", 5f);
        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}