using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovesCounter : MonoBehaviour
{
    public TextMeshProUGUI movesTXT;
    private CarController carController;

    void Start()
    {
        carController = FindFirstObjectByType(typeof(CarController)) as CarController;
    }

    void Update()
    {
        movesTXT.text = "Jogadas: " + carController.Moves;
    }
}
