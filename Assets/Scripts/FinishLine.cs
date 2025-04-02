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

            CarController car = other.GetComponent<CarController>();
            Debug.Log("Vitória! Jogadas totais: " + car.Moves);

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