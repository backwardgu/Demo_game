using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New item")]
public class Item_template : ScriptableObject
{
    public string name;
    [TextArea]
    public string description;
    public Sprite sprite;
    public bool is_gold;

    [Header("效果")]
    public int current_health;
    public int max_health;
    public int value;
    public int speed;
    public int damage;

    [Header("持续时间")]
    public float existing_time;
}
