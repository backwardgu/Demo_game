using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loot : MonoBehaviour
{
    public Item_template item;
    public SpriteRenderer sr;
    public Animator animator;
    public int number;

    public static System.Action<loot, int> On_Item_picked;
    private void OnValidate()
    {
        if (item == null) return;
        sr.sprite = item.sprite;
        this.name = item.name; 
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")||collision.CompareTag("Player_archer"))
        {
            On_Item_picked?.Invoke(this, number);
        }
    }
    public void end_picking()
    {
        Destroy(gameObject);
    }
}
