using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy_health : MonoBehaviour
{
    public int cur_health;
    public int max_health;
    public int exp_award;
    public delegate void Monster_defeat(int exp);
    public static event Monster_defeat On_Monster_deteated;


    void Start()
    {
        max_health = 5;
        cur_health = 5;
        exp_award = 10;
    }
    
    public void Change_health(int amount)
    {
        Debug.Log($"怪物生命值变动，变动为{amount}");
        cur_health += amount;
        if (cur_health > max_health)
        {
            cur_health = max_health;
        }
        if (cur_health <= 0)
        {
            On_Monster_deteated(exp_award);
            Destroy(gameObject);

        }
    }
}
