using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager_02 : MonoBehaviour
{
    public static GameManager_02 Instance { get; private set; }

    private bool isPlayer, isGameFinished;

    public TextMeshProUGUI message;

    private Board_02 myBoard;

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

        isPlayer = true;
        isGameFinished = false;
        myBoard = new Board_02();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeText();
        }
    }

    private void ChangeText()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

        if (hit.collider != null && !hit.collider.gameObject.GetComponent<Click_02>().hasPlayed && !isGameFinished)
        {
            hit.collider.gameObject.GetComponent<Click_02>().SetStore(isPlayer);

            if (isPlayer)
            {
                message.text = "O's turn";
            }
            else
            {
                message.text = "X's turn";
            }

            isPlayer = !isPlayer;
        }
    }

    public void DetermineWinner(int row, int column, bool isPlayer)
    {
        if (myBoard.UpdateCell(row, column, isPlayer))
        {
            isGameFinished = true;
            message.text = isPlayer ? "X Wins" : "O Wins";
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene(2);
    }

    public void GameQuit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
