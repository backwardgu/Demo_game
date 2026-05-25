using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

enum cur_scene
{
    explore,
    shopping
}
public class Inventory_Slot : MonoBehaviour,IPointerClickHandler
{
    public Item_template item;
    public Image item_icon;
    public TMP_Text quantity_text;
    public int quantity = 0;
    public Transform Player;
    public GameObject Loot_Prefab;
    public Use_item using_item;

    cur_scene status;
    ShopManager cur_shop;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("触发点击");
        if (quantity <= 0 || item == null) return;
        switch(status)
        {
            case cur_scene.explore:
                explore_click(eventData);
                break;
            case cur_scene.shopping:
                shopping_click(eventData);
                break;
        }


    }
    public void OnEnable()
    {
        ShopManager.On_Shop_Open += handle_shop;
    }
    public void OnDisable()
    {
        ShopManager.On_Shop_Open -= handle_shop;
    }
    public void UpdateUI()
    {
        if(item!= null && quantity > 0)
        {
            item_icon.sprite = item.sprite;
            item_icon.gameObject.SetActive(true);
            quantity_text.text = quantity.ToString();
        }
        else
        {
            item = null;
            item_icon.gameObject.SetActive(false);
            quantity_text.text = "";
        }
    }
    public void consume_item()
    {
        if (item == null) return;

        using_item.use(item);
        quantity--;
        UpdateUI();
    }
    public void drop_item()
    {
        Loot abandoned_loot = Instantiate(Loot_Prefab,Player.position, Quaternion.identity).GetComponent<Loot>();
        abandoned_loot.Initialize(quantity, item);
        abandoned_loot.Loot_Drop();
        quantity = 0;
        item = null;
        UpdateUI();
    }
    void handle_shop(ShopManager shop, bool is_open)
    {
        if (is_open)
        {
            cur_shop = shop;
            status = cur_scene.shopping;
        }
        else
        {
            cur_shop = null;
            status = cur_scene.explore;
        }
    }
    void explore_click(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            consume_item();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            drop_item();
        }
    }
    void shopping_click(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            return;
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if(!cur_shop.Sell_item(item))return;
            quantity--;
            UpdateUI();
        }
    }
}
