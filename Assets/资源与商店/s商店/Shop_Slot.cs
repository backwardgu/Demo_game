using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop_Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerMoveHandler
{
    public Item_template item;
    public TMP_Text item_name;
    public TMP_Text price_text;
    public Image icon;
    public int price;
    public ShopManager manager;
    public Button button;
    public Item_Info item_info;
    public void Initialize(Item_template item,int price)
    {
        this.item = item;
        item_name.text = item.name;
        this.price_text.text = price.ToString(); 
        this.price = price;
        icon.sprite = item.sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        item_info.ShowInfo(item);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        item_info.HideInfo();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        item_info.FollowMouse();
    }

    public void On_buy_clicked()
    {
        Debug.Log("试图购买");
        manager.Buy_item(this);
    }
}
