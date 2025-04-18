using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Choices_01 { ROCK, PAPER, SCISSORS, NONE }

public class GameManager_01 : MonoBehaviour
{
    public static GameManager_01 Instance;

    const string win = "Wins!";
    const string draw = "The game is Draw!";

    public Text winningMessageText;
    public GameObject settings;
    public Text playerNameInput;
    public Text opponentNameInput;
    public Toggle toggle_AI;

    private bool isPlayerSelected, isOpponentSelected, isGameFinished, isOpponentAI;
    private string playerName, opponentName;
    private Choices_01 playerChoice = Choices_01.NONE;
    private Choices_01 opponentChoice = Choices_01.NONE;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerName = GameData_01.Instance.playerName;
        opponentName = GameData_01.Instance.opponentName;
        isOpponentAI = GameData_01.Instance.isOpponentAI; // Set to true for AI opponent
        settings.SetActive(false);
    }

    public void Select(Choices_01 myChoice, bool isPlayer)
    {
        if (isGameFinished) return;

        if (isPlayer)
        {
            playerChoice = myChoice;
            isPlayerSelected = true;

            if (isOpponentAI)
            {
                Select((Choices_01)Random.Range(0, 3), false);
            }
        }
        else
        {
            opponentChoice = myChoice;
            isOpponentSelected = true;
        }

        if (isPlayerSelected && isOpponentSelected)
        {
            isGameFinished = true;
            DetermineWinner();
        }   
    }
    
    public void DetermineWinner()
    {
        if (playerChoice == opponentChoice)
        {
            winningMessageText.text = draw;
        }
        else if ((playerChoice == Choices_01.ROCK && opponentChoice == Choices_01.SCISSORS) ||
                 (playerChoice == Choices_01.PAPER && opponentChoice == Choices_01.ROCK) ||
                 (playerChoice == Choices_01.SCISSORS && opponentChoice == Choices_01.PAPER))
        {
            winningMessageText.text = $"{playerName} {win}";
        }
        else
        {
            winningMessageText.text = $"{opponentName} {win}";
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowSettings()
    {
        toggle_AI.isOn = isOpponentAI;
        playerNameInput.text = playerName;
        opponentNameInput.text = opponentName;
        settings.SetActive(true);
    }

    public void HideSettings()
    {
        settings.SetActive(false);
    }

    public void ChangeAI(bool isAI)
    {
        GameData_01.Instance.isOpponentAI = isAI;
        isOpponentAI = isAI;
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
        GameData_01.Instance.playerName = name;
    }

    public void SetOpponentName(string name)
    {
        opponentName = name;
        GameData_01.Instance.opponentName = name;
    }
}
