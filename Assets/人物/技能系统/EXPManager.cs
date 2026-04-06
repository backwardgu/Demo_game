using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EXPManager : MonoBehaviour
{
    public int level;
    public int cur_exp;
    public int next_level_exp;
    public Player_health health;
    public Slider expSlider;
    public TMP_Text cur_text;
    public static event System.Action<int> OnUpgrade;
    private void Start()
    {
        level = 1;
        cur_exp = 0;
        next_level_exp = 10;
        update_UI();
    }
    private void Update()
    {
        if(Input.GetKeyDown("return"))
        {
            GainEXP(2);
        }
    }
    public void GainEXP(int exp)
    {
        cur_exp += exp;
        if(cur_exp>=next_level_exp)
        {
            upgrade();
        }
        update_UI();
    }
    private void upgrade()
    {
        level++;
        cur_exp -= next_level_exp;
        next_level_exp += 10;
        health.Change_health(StatusManager.Instance.maxHealth);
        OnUpgrade?.Invoke(1);
    }
    public void update_UI()
    {
        expSlider.maxValue = next_level_exp;
        expSlider.value = cur_exp;
        cur_text.text = "Level:" + level;
    }
    private void OnEnable()
    {
        Enemy_health.On_Monster_deteated += GainEXP;
    }
    private void OnDisable()
    {
        Enemy_health.On_Monster_deteated -= GainEXP;
    }
}
