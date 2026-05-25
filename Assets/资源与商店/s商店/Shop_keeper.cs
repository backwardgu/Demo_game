using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_keeper : MonoBehaviour
{
    public Animator UI_animator;
    public bool player_in_range;
    public bool canva_open;
    public CanvasGroup canvas;
    public ShopManager shop;
    public GameObject Shop_UI;
    void Update()
    {
        if (!player_in_range) return;

        if (Input.GetButtonDown("Submit"))
        {
            if (canva_open)
            {
                canvas.alpha = 0;
                canva_open = false;
                canvas.interactable = true;
                Time.timeScale = 1;
            }
            else
            {
                canvas.alpha = 1;
                canvas.interactable = true;
                canva_open = true;
                Time.timeScale = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Shop_UI.SetActive(true);
            UI_animator.SetBool("is_available", true);
            player_in_range = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UI_animator.SetBool("is_available", false);
            player_in_range = false;
            Shop_UI.SetActive(false);
        }
    }
}
