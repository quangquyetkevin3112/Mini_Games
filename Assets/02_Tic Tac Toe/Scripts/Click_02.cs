using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Click_02 : MonoBehaviour
{
    public int row, column;
    public TextMeshProUGUI text;
    public bool hasPlayed;

    private void Awake()
    {
        hasPlayed = false;
    }

    public void SetStore(bool isPlayer)
    {
        if (isPlayer)
        {
            text.text = "X";
            text.color = new Color(1, 0, 0);
        }
        else
        {
            text.text = "O";
            text.color = new Color(0, 1, 0);
        }

        GameManager_02.Instance.DetermineWinner(row, column, isPlayer);
        hasPlayed = true;
    }
}
