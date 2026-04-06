using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public Player_health health;
    private void OnEnable()
    {
        SkillSlot.Skill_points_changed += upgrade_skill;
    }
    private void OnDisable()
    {
        SkillSlot.Skill_points_changed -= upgrade_skill;
    }
    public void upgrade_skill(SkillSlot slot)
    {
        string skillName = slot.skillso.skillname;
        switch(skillName)
        {
            case "最大生命值提升":
                health.Change_Max_Health(2);
                break;
            case "攻击力提升":
                StatusManager.Instance.damage += 1;
                break;
            default:
                Debug.Log("未知技能错误！" + skillName);
                break;
        }
    }
}
