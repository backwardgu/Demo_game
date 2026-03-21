using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_health : MonoBehaviour
{
    public int cur_health;
    public int max_health;
    public TMP_Text health_text;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        max_health = 5;
        cur_health = 5;
        health_text.text = "HP:" + cur_health + "/" + max_health;
    }

    public void Change_health(int amount)
    {
        Debug.Log($"生命值变动，变动为{amount}");
        cur_health += amount;
        animator.Play("HP_UI");
        health_text.text = "HP:" + cur_health + "/" + max_health;
        if (cur_health > max_health)
        {
            cur_health = max_health;
        }
        if (cur_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
