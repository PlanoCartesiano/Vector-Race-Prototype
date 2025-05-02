using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandDetector : MonoBehaviour
{
    private CarController car;

    void Awake()
    {
        car = GetComponent<CarController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sand"))
            car.SetInSand(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sand"))
            car.SetInSand(false);
    }
}
