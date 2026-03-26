using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy_health : MonoBehaviour
{
    public int cur_health;
    public int max_health;

    void Start()
    {
        max_health = StatusManager.Instance.max_health;
        cur_health = StatusManager.Instance.cur_health;
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
            gameObject.SetActive(false);
        }
    }
}
