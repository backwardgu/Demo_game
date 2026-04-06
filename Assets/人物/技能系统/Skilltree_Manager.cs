using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class Skilltree_Manager : MonoBehaviour
{
    public SkillSlot[] skillSlots;
    public TMP_Text skill_texts;

    public int available_points;
    public int max_points;


    private void Start()
    {
        foreach(var slot in skillSlots)//auto?
        {
            slot.skill_button.onClick.AddListener(()=>CheckAvailablePoints(slot));
        }
        max_points = 5;
        available_points = 3;
        Update_ability_points(0);
    }
    private void CheckAvailablePoints(SkillSlot slot)
    {
        if(available_points>0)
        {
            slot.update_skill();
        }
    }

    public void OnEnable()
    {
        SkillSlot.Skill_points_changed += handle_ability_changed;
        SkillSlot.Skill_Maxed += handle_skill_maxed;
        EXPManager.OnUpgrade += Update_ability_points;
    }
    public void OnDisable()
    {
        SkillSlot.Skill_points_changed -= handle_ability_changed;
        SkillSlot.Skill_Maxed -= handle_skill_maxed;
        EXPManager.OnUpgrade += Update_ability_points;
    }
    public void Update_ability_points(int points)
    {
        available_points += points;
        if(points >0)max_points += points;
        skill_texts.text = "Skill points:"+available_points+"/"+max_points;
    }

    public void handle_ability_changed(SkillSlot slot)
    {
        if(available_points > 0)
        {
            Update_ability_points(-1);
        }
    }
    public void handle_skill_maxed(SkillSlot slot)
    {
        foreach(SkillSlot singleslot in skillSlots)
        {
            singleslot.unlock();
        }
    }
}
