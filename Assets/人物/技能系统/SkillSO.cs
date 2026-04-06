using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewSkill",menuName = "SkillTree/Skill")]
public class SkillSO : ScriptableObject
{
    public string skillname;
    public int max_level;
    public Sprite skill_icon;
}
