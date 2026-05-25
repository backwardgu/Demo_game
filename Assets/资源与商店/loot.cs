using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public Item_template item;
    public SpriteRenderer sr;
    public Animator animator;
    public int number;
    public bool can_be_picked = true;

    public static System.Action<Loot, int> On_Item_picked;
    private void OnValidate()
    {
        if (item == null) return;
        Update_Appearance();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(can_be_picked && (collision.CompareTag("Player")||collision.CompareTag("Player_archer")) )
        {
            On_Item_picked?.Invoke(this, number);
        }
    }
    public void Initialize(int quantity,Item_template new_item)
    {
        number = quantity;
        item = new_item;
        Update_Appearance();
    }
    public void end_picking()
    {
        Destroy(gameObject);
    }
    public void Update_Appearance()
    {
        sr.sprite = item.sprite;
        this.name = item.name;
    }
    public void Loot_Drop()
    {
        //animator.Play("Loot_Drop");
        StartCoroutine(pick_cool_down(3));
    }
    IEnumerator pick_cool_down(float time)
    {
        can_be_picked = false;
        yield return new WaitForSeconds(time);
        can_be_picked = true;
    }

    public void OnDropAnimationEnd()
    {
        Debug.Log("物品掉落终止");
        // 动画结束时的位置已经在动画中设定好
        // 什么都不做，或者记录当前位置
        // 关键在于：这帧之后动画不会再覆盖位置
    }
}
