using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Inventory_Manager : MonoBehaviour
{
    public int gold_count;
    public Animator animator;
    public TMP_Text gold_text;
    public Inventory_Slot[] slots;

    public void Update()
    {
        if(Input.GetButtonDown("Item1"))
            Use_item(0);
        if (Input.GetButtonDown("Item2"))
            Use_item(1);
        if (Input.GetButtonDown("Item3"))
            Use_item(2);
    }
    private void OnEnable()
    {
        Loot.On_Item_picked += Add_Item;
    }
    private void OnDisable()
    {
        Loot.On_Item_picked -= Add_Item;
    }
    public void Add_Item(Loot loot, int number)
    {
        Debug.Log("拾取了一个" + loot.name);
        if (loot.item.is_gold)
        {
            gold_change(loot.number);
            loot.animator.Play("Loot_picked");
            return;
        }

        while(number > 0)
        {
            Inventory_Slot available_slot = find_slot(loot.item, number);
            
            if (available_slot == null) break;

            //处理数量加减逻辑
            int available_space = loot.item.stack_size - available_slot.quantity;
            int final_space = Math.Min(available_space, number);
            add_item(available_slot,loot.item, final_space);
            number -= final_space;

            loot.animator.Play("Loot_picked");
        }
    }
    public Inventory_Slot find_slot(Item_template item,int number)
    {
        Inventory_Slot target_slot = null;

        foreach (Inventory_Slot slot in slots)
        {
            if (slot.item != item) continue;

            int available_space = slot.item.stack_size - slot.quantity;
            if (available_space > 0)
            {
                return slot;
            }
        }

        foreach (Inventory_Slot slot in slots)
        {
            if (slot.item == null) return slot;
        }

        Debug.Log("物品栏已满");
        return target_slot;
    }
    public void add_item(Inventory_Slot target_slot,Item_template item,int number)
    {
        target_slot.item = item;
        target_slot.quantity += number;
        target_slot.UpdateUI();
    }

    public void gold_change(int num)
    {
        gold_count += num;
        gold_count = Math.Max(gold_count, 0);
        gold_text.text = gold_count.ToString();
        animator.Play("Gold_animation");
    }
    public void Use_item(int i)
    {
        if (slots[i].item == null) return;
        slots[i].consume_item();
    }
}
