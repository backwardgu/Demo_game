using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillSlot : MonoBehaviour
{
    public SkillSO skillso;
    public Image skillicon;
    public Button skill_button;
    public TMP_Text skillLeveltext;
    public List<SkillSlot> pre_skills;

    public int current_level;
    public int max_level;

    public bool is_unlocked = false;

    public static System.Action<SkillSlot> Skill_points_changed;
    public static System.Action<SkillSlot> Skill_Maxed;
    private void OnValidate()
    {
        if(skillso != null)
        {

            UpdateUI();
        }
    }
    private void UpdateUI()
    {
        skillicon.sprite = skillso.skill_icon;

        if (is_unlocked)
        {
            skill_button.interactable = true;
            skillLeveltext.text = current_level.ToString()+ "/" + skillso.max_level.ToString();
            skillicon.color = Color.white;
        }
        else
        {
            skill_button.interactable = false;
            skillLeveltext.text = "Locked";
            skillicon.color = Color.grey;
        }
    }

    public void update_skill()
    {
        if(is_unlocked && current_level< skillso.max_level)
        {
            current_level++;
            UpdateUI();
            Skill_points_changed?.Invoke(this);
            if(current_level == skillso.max_level)
            {
                Skill_Maxed?.Invoke(this);
            }
        }
    }
    public void unlock()
    {
        if (can_unlock())
        {
            is_unlocked = true;
            UpdateUI();
        }
    }
    public bool can_unlock()
    {
        foreach(SkillSlot slot in pre_skills)
        {
            if(slot.current_level < slot.skillso.max_level || !slot.is_unlocked)
            {
                return false;
            }
        }
        return true;
    }
}
