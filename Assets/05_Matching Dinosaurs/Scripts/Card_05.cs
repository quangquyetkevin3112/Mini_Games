using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_05 : MonoBehaviour
{
    public bool hasClicked, hasTurnFinished;
    public int index;
    public int dino;
    public Sprite unrevealed;
    public List<Sprite> dinos;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Start()
    {
        hasClicked = false;
        hasTurnFinished = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        dino = GameManager_05.Instance.myBoard.GetIndex(index);
        spriteRenderer.sprite = unrevealed;
    }

    public void UpdateTurn()
    {
        hasClicked = true;
        //UpdateImage();
        animator.Play("Reveal", -1, 0f);
    }

    public void UpdateImage()
    {
        spriteRenderer.sprite = dinos[dino];
    }

    public void RemoveTurn()
    {
        hasClicked = false;
        //RemoveImage();
        animator.Play("UnReveal", -1, 0f);
    }

    public void RemoveImage()
    {
        spriteRenderer.sprite = unrevealed;
    }
}
