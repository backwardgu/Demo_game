using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class Inventory_Manager : MonoBehaviour
{
    public int gold_count;
    public Animator animator;
    public TMP_Text gold_text;
    public Inventory_Slot[] slots;
    public Use_item use;

    public void Update()
    {
        if(Input.GetButtonDown("Item1"))
            Use_item(0);
    }
    private void OnEnable()
    {
        loot.On_Item_picked += Add_Item;
    }
    private void OnDisable()
    {
        loot.On_Item_picked -= Add_Item;
    }
    public void Add_Item(loot Loot, int number)
    {
        Debug.Log("拾取了一个" + Loot.name);
        if (Loot.item.is_gold)
        {
            gold_count += number;
            Loot.animator.Play("Loot_picked");
            gold_text.text = gold_count.ToString();
            animator.Play("Gold_animation");
            return;
        }
        else
        {
            foreach (Inventory_Slot slot in slots)
            {
                if (slot.item != null) continue;


                slot.item = Loot.item;
                Loot.animator.Play("Loot_picked");
                slot.quantity += number;
                slot.UpdateUI();
                return;
            }
            Debug.Log("物品栏已满");
            //Update_UI();
        }
    }
    public void Use_item(int i)
    {
        if (slots[i].item == null) return;
        use.use(slots[i].item);
        if (--slots[i].quantity <= 0)
        {
            slots[i].item = null;
        }
        slots[i].UpdateUI();
    }
}
