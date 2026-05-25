using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_health : MonoBehaviour
{
    public TMP_Text health_text;
    public Animator animator_UI,animator_player;
    // Start is called before the first frame update
    void Start()
    {
        StatusManager.Instance.currentHealth = StatusManager.Instance.maxHealth;
        health_text.text = "HP:" + StatusManager.Instance.currentHealth + "/" + StatusManager.Instance.maxHealth;
    }

    private void OnEnable()
    {
        StatusManager.Instance.OnStatChanged += health_change;
        StatusManager.Instance.OnStatChanged += die;
    }
    private void OnDisable()
    {
        StatusManager.Instance.OnStatChanged -= health_change;
        StatusManager.Instance.OnStatChanged -= die;
    }
    public void health_change(string status,object amount)
    {
        animator_UI.Play("HP_UI");
        health_text.text = "HP:" + StatusManager.Instance.currentHealth + "/" + StatusManager.Instance.maxHealth;
    }

    //虽然没有必要，但是我想测试一下事件系统
    public void die(string name,object value)
    {
        if (name != "cur_health") return;
        if((int)value <=0)animator_player.SetBool("is_alive",false);
    }

    //动画事件中调用好让角色死亡
    public void remove()
    {
        Destroy(gameObject);
    }
}
