using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishLine : MonoBehaviour
{
    public GameObject victorySpam;

    private Collider2D finishCollider;
    private HashSet<CarController> exitedCars = new HashSet<CarController>();
    private bool triggerActivated = false;

    private void Start()
    {
        if (victorySpam != null)
            victorySpam.SetActive(false);

        finishCollider = GetComponent<Collider2D>();
        finishCollider.isTrigger = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (triggerActivated) return;

        CarController car = other.GetComponent<CarController>();
        if (car != null)
        {
            exitedCars.Add(car);

            if (exitedCars.Count >= 2)
            {
                Debug.Log("[FinishLine] Ambos os carros saíram da linha, ativando trigger.");
                finishCollider.isTrigger = true;
                triggerActivated = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!triggerActivated) return;

        if (other.CompareTag("Player"))
        {

            CarController car = other.GetComponent<CarController>();
            Debug.Log("Vitória! Jogadas totais: " + car.Moves);

            if (car != null && !car.HasFinished)
            {
                car.MarkAsFinished();
                TurnManager.Instance.CheckForVictory();
            }
        }
    }
}