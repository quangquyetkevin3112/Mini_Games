using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData_01 : MonoBehaviour
{
    public static GameData_01 Instance;

    public string playerName;
    public string opponentName;
    public bool isOpponentAI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            playerName = "Player";
            opponentName = "Opponent";
            isOpponentAI = true; // Set to true for AI opponent
            DontDestroyOnLoad(gameObject);
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
