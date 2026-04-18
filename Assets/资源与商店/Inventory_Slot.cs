using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Slot : MonoBehaviour
{
    public Item_template item;
    public Image item_icon;
    public TMP_Text quantity_text;
    public int quantity = 0;
    public void UpdateUI()
    {
        if(item!= null)
        {
            item_icon.sprite = item.sprite;
            item_icon.gameObject.SetActive(true);
            quantity_text.text = quantity.ToString();
        }
        else
        {
            item_icon.gameObject.SetActive(false);
            quantity_text.text = "";
        }
    }
}
