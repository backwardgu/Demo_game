using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_health : MonoBehaviour
{
    public TMP_Text health_text;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        StatusManager.Instance.currentHealth = StatusManager.Instance.currentHealth;
        health_text.text = "HP:" + StatusManager.Instance.currentHealth + "/" + StatusManager.Instance.maxHealth;
        StatusManager.Instance.OnStatChanged += die;
    }

    public void Change_health(int amount)
    {
        Debug.Log($"生命值变动，变动为{amount}");
        StatusManager.Instance.currentHealth += amount;
        animator.Play("HP_UI");
        health_text.text = "HP:" + StatusManager.Instance.currentHealth + "/" + StatusManager.Instance.maxHealth;
        if (StatusManager.Instance.currentHealth > StatusManager.Instance.maxHealth)
        {
            StatusManager.Instance.currentHealth = StatusManager.Instance.maxHealth;
        }
    }

    //虽然没有必要，但是我想测试一下事件系统
    public void die(string name,object value)
    {
        if (name != "cur_health") return;
        if((int)value <=0) gameObject.SetActive(false);
    }
}
