using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryMessage : MonoBehaviour
{
    public static VictoryMessage Instance;

    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private TextMeshProUGUI victoryText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        victoryPanel.SetActive(false);
    }

    public void ShowVictory(string message)
    {
        victoryText.text = message;
        victoryPanel.SetActive(true);
    }
}
