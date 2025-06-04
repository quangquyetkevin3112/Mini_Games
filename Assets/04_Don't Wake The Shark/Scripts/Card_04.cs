using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_04 : MonoBehaviour
{
    public bool hasClicked;
    public int row, col;
    public Sprite fish, gold, shark, unrevealed;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public Choice_04 myChoice;

    private void Start()
    {
        hasClicked = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        myChoice = GameManager_04.Instance.myBoard.GetChoice(row, col);
        spriteRenderer.sprite = unrevealed;
    }

    public void PlayTurn()
    {
        animator.Play("Reveal");
        hasClicked = true;
    }

    public void ChangeImage()
    {
        Sprite current = myChoice == Choice_04.Fish ? fish
                       : myChoice == Choice_04.Gold ? gold
                       : shark;
        spriteRenderer.sprite = current;
    }
}
