using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_05 : MonoBehaviour
{
    public static GameManager_05 Instance { get; private set; }

    public Board_05 myBoard;

    private bool isGameFinished, isFirstTurn;
    private Card_05 first;

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

        isFirstTurn = true;
        isGameFinished = false;
        myBoard = new Board_05();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGameFinished)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

            if (hit.collider && hit.collider.CompareTag("Card"))
            {
                if (hit.collider.gameObject.GetComponent<Card_05>().hasTurnFinished) return;

                if (isFirstTurn)
                {
                    first = hit.collider.gameObject.GetComponent<Card_05>();
                    first.UpdateTurn();
                }
                else
                {
                    Card_05 second = hit.collider.gameObject.GetComponent<Card_05>();

                    if (second.hasClicked)
                    {
                        first.RemoveTurn();
                        second.RemoveTurn();
                        isFirstTurn = !isFirstTurn;
                        return;
                    }

                    second.UpdateTurn();

                    if (first.dino == second.dino)
                    {
                        first.hasTurnFinished = true;
                        second.hasTurnFinished = true;

                        if (myBoard.UpdateChoice())
                        {
                            isGameFinished = true;
                            return;
                        }

                        isFirstTurn = !isFirstTurn;
                        return;
                    }

                    first.RemoveTurn();
                    second.RemoveTurn();
                }

                isFirstTurn = !isFirstTurn;
            }
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene(5);
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
