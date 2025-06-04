using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager_04 : MonoBehaviour
{
    public static GameManager_04 Instance { get; private set; }

    public TextMeshProUGUI message;

    private bool isGameFinished;

    public Board_04 myBoard;
    
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

        message.text = "Play The Next Turn";
        isGameFinished = false;
        myBoard = new Board_04();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !isGameFinished)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

            if (hit.collider && hit.collider.CompareTag("Card"))
            {
                Card_04 card = hit.collider.gameObject.GetComponent<Card_04>();

                if (card.hasClicked) return;

                card.PlayTurn();

                if (card.myChoice == Choice_04.Gold)
                {
                    isGameFinished = true;
                    message.text = "You Win!";
                }
                else if (card.myChoice == Choice_04.Shark)
                {
                    isGameFinished = true;
                    message.text = "You Lose...";
                }
            }
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene(4);
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
