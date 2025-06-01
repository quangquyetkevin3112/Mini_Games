using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Choice_01 { Rock, Paper, Scissors, None }

public class GameManager_01 : MonoBehaviour
{
    public static GameManager_01 Instance { get; private set; }

    const string win = " Win!";
    const string draw = "The game is Draw.";

    private Choice_01 playerChoice = Choice_01.None;
    private Choice_01 opponentChoice = Choice_01.None;
    private bool isPlayerSelected = false;
    private bool isOpponentSelected = false;
    private bool isGameFinished = false;
    private bool isOpponentAI = false;
    private string playerName;
    private string opponentName;

    public TextMeshProUGUI resultText;
    public GameObject settingsBG;
    public TextMeshProUGUI playerNameInput;
    public TextMeshProUGUI opponentNameInput;

    public Toggle toggle_isAI;

    public Image playerImage;
    public Image opponentImage;
    public Sprite paperSprite;
    public Sprite rockSprite;
    public Sprite scissorsSprite;
    public Animator _playerChoice;
    public Animator _opponentChoice;
    public Animator _playerSelect;
    public Animator _opponentSelect;

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
        isOpponentAI = GameData_01.Instance.isOpponentAI;
        settingsBG.SetActive(false);
    }

    public void Select(Choice_01 myChoice, bool isPlayer)
    {
        if (isGameFinished) return;

        if (isPlayer)
        {
            playerChoice = myChoice;
            isPlayerSelected = true;

            if (isOpponentAI)
            {
                Select((Choice_01)Random.Range(0, 3), false);
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

    private void DetermineWinner()
    {
        if (playerChoice == opponentChoice)
        {
            resultText.text = draw;
        }
        else if (playerChoice == Choice_01.Rock && opponentChoice == Choice_01.Scissors ||
        playerChoice == Choice_01.Paper && opponentChoice == Choice_01.Rock ||
        playerChoice == Choice_01.Scissors && opponentChoice == Choice_01.Paper)
        {
            resultText.text = playerName + win;
        }
        else
        {
            resultText.text = opponentName + win;
        }

        SetImage();
        SetAnimations();
    }

    private void SetImage()
    {
        SetImage(playerImage, playerChoice);
        SetImage(opponentImage, opponentChoice);
    }

    private void SetImage(Image target, Choice_01 myChoice)
    {
        switch (myChoice)
        {
            case Choice_01.Paper:
                target.sprite = paperSprite;
                break;
            case Choice_01.Rock:
                target.sprite = rockSprite;
                break;
            case Choice_01.Scissors:
                target.sprite = scissorsSprite;
                break;
        }
    }

    private void SetAnimations()
    {
        _playerChoice.Play("PlayerChoiceMove");
        _opponentChoice.Play("OpponentChoiceMove");
        _playerSelect.Play("PlayerSelectedMove");
        _opponentSelect.Play("OpponentSelectedMove");
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenSettings()
    {
        toggle_isAI.isOn = isOpponentAI;
        playerNameInput.text = playerName;
        opponentNameInput.text = opponentName;
        settingsBG.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsBG.SetActive(false);
    }

    public void ChangeAI(bool isAI)
    {
        GameData_01.Instance.isOpponentAI = isAI;
        isOpponentAI = isAI;
    }

    public void SetPlayerName(string inputName)
    {
        playerName = inputName;
        GameData_01.Instance.playerName = inputName;
    }

    public void SetOpponentName(string inputName)
    {
        opponentName = inputName;
        GameData_01.Instance.opponentName = inputName;
    }
}
