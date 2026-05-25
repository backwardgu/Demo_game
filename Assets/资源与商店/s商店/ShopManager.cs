using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] public List<Shop_Item> shop_list;

    [SerializeField] public Shop_Slot[] slots;

    [SerializeField] public Inventory_Manager inventory;

    public static event System.Action<ShopManager,bool> On_Shop_Open;
    public void Start()
    {
        Update_Shop_Items();
        On_Shop_Open ?.Invoke(this,true);
    }
    public void Update_Shop_Items()
    {
        for (int i = 0; i < slots.Length && i < shop_list.Count; i++)
        {
            Shop_Item cur_item = shop_list[i];
            slots[i].Initialize(cur_item.item, cur_item.price);
            slots[i].gameObject.SetActive(true);
        }
        for (int i = shop_list.Count; i < slots.Length; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
    }
    public void Buy_item(Shop_Slot item)
    {
        Inventory_Slot slot = inventory.find_slot(item.item, 1);
        if (slot == null || inventory.gold_count < item.price) return;
        
        int index = Array.IndexOf(slots, item);
        shop_list.RemoveAt(index);
        Debug.Log("购买成功！");
        inventory.add_item(slot,item.item,1);
        inventory.gold_change(-item.price);
        Update_Shop_Items();
    }

    public bool Sell_item(Item_template item)
    {
        foreach(Shop_Slot slot in slots)
        {
            if(shop_list.Count < slots.Length)
            {
                Debug.Log("售出成功！");
                Shop_Item new_item = new Shop_Item(item, item.price);
                shop_list.Add(new_item);
                Update_Shop_Items();

                inventory.gold_change(item.price/2);

                return true;
            }
        }
        return false;
    }
}


[System.Serializable]
public class Shop_Item
{
    public Item_template item;
    public int price;
    public Shop_Item(Item_template item, int price)
    {
        this.item = item;
        this.price = price;
    }
};
