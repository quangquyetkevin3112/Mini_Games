using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceSelect_01 : MonoBehaviour
{
    public Choice_01 buttonChoice;
    public bool isPlayer;

    public void ChoiceSelected()
    {
        GameManager_01.Instance.Select(buttonChoice, isPlayer);
    }
}
