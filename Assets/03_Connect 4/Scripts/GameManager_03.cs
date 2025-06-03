using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager_03 : MonoBehaviour
{
    public GameObject red, green;

    private bool isPlayer, isGameFinished;

    public TextMeshProUGUI turnMessage;

    const string RED_MESSAGE = "Red's Turn";
    const string GREEN_MESSAGE = "Green's Turn";

    Color RED_COLOR = new Color(231, 29, 54, 255) / 255f;
    Color GREEN_COLOR = new Color(0, 222, 1, 255) / 255f;

    private Board_03 myBoard;

    private void Awake()
    {
        isPlayer = true;
        isGameFinished = false;
        turnMessage.text = RED_MESSAGE;
        turnMessage.color = RED_COLOR;
        myBoard = new Board_03();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGameFinished)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

            if (hit.collider && hit.collider.CompareTag("Press"))
            {
                // Check out of bounds
                if (hit.collider.gameObject.GetComponent<Column_03>().targetLocation.y > 1.5f) return;

                // Spawn the GameObject
                Vector3 spawnPos = hit.collider.gameObject.GetComponent<Column_03>().spawnLocation;
                Vector3 targetPos = hit.collider.gameObject.GetComponent<Column_03>().targetLocation;
                GameObject circle = Instantiate(isPlayer ? red : green);
                circle.transform.position = spawnPos;
                circle.GetComponent<Mover_03>().targetPosition = targetPos;

                // Increase the target location height
                hit.collider.gameObject.GetComponent<Column_03>().targetLocation =
                    new Vector3(targetPos.x, targetPos.y + 0.7f, targetPos.z);

                myBoard.UpdateBoard(hit.collider.gameObject.GetComponent<Column_03>().column - 1, isPlayer);

                if (myBoard.Result(isPlayer))
                {
                    turnMessage.text = (isPlayer ? "Red" : "Green") + " Wins!";
                    isGameFinished = true;
                    return;
                }

                // Update turn message
                turnMessage.text = !isPlayer ? RED_MESSAGE : GREEN_MESSAGE;
                turnMessage.color = !isPlayer ? RED_COLOR : GREEN_COLOR;

                // Change player turn
                isPlayer = !isPlayer;
            }
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene(3);
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
