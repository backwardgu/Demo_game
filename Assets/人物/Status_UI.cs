using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System.Net.NetworkInformation;

public class Status_UI : MonoBehaviour
{
    public GameObject[] statslots;
    public CanvasGroup canvas;
    private bool is_canva_open;
    void Start()
    {
        StatusManager.Instance.OnStatChanged += UpdateStats;
        UpdateStats(StatusManager.Instance);
        is_canva_open = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("ESC"))
        {
            if (is_canva_open)
            {
                Time.timeScale = 1;
                canvas.alpha = 0;
                is_canva_open = false;
            }

            else
            {
                Time.timeScale = 0;
                canvas.alpha = 1;
                is_canva_open = true;
            }
        } 
    }

    public void UpdateStats(string stats_name,object value)
    {
        switch(stats_name)
        {
            case "damage": 
                statslots[0].GetComponentInChildren<TMP_Text>().text = "damage:" + value;
                break;
            case "weapon_range":
                statslots[1].GetComponentInChildren<TMP_Text>().text = "range:" + value;
                break;
            case "hitback":
                statslots[2].GetComponentInChildren<TMP_Text>().text = "hit back:" + value;
                break;
            case "speed":
                statslots[3].GetComponentInChildren<TMP_Text>().text = "speed:" + value;
                break;
            case "maxHealth":
                statslots[4].GetComponentInChildren<TMP_Text>().text = "Max HP:" + value;
                break;
            case "cur_health":
                statslots[5].GetComponentInChildren<TMP_Text>().text = "HP:" + value;
                break;
        }
    }
    public void UpdateStats(StatusManager status)
    {
        // 直接从 status 实例读取属性值
        statslots[0].GetComponentInChildren<TMP_Text>().text = "damage:" + status.damage;
        statslots[1].GetComponentInChildren<TMP_Text>().text = "range:" + status.weaponRange;
        statslots[2].GetComponentInChildren<TMP_Text>().text = "hit back:" + status.hitback;
        statslots[3].GetComponentInChildren<TMP_Text>().text = "speed:" + status.speed;
        statslots[4].GetComponentInChildren<TMP_Text>().text = "Max HP:" + status.maxHealth;
        statslots[5].GetComponentInChildren<TMP_Text>().text = "HP:" + status.currentHealth;
    }
    private void OnDestroy()
    {
        StatusManager.Instance.OnStatChanged -= UpdateStats;
    }
}
