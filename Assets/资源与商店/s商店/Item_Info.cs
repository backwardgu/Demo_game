using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item_Info : MonoBehaviour
{
    public CanvasGroup infoPanel;
    public TMP_Text item_name;
    public TMP_Text item_desecription_text;

    [Header("属性明细")]
    public TMP_Text item_effect;

    private RectTransform infoPanelrect;

    private void Awake()
    {
        infoPanelrect = GetComponent<RectTransform>();
    }
    public void ShowInfo(Item_template item)
    {
        infoPanel.alpha = 1f;

        item_name.text = item.name;
        item_desecription_text.text = item.description;
        item_effect.text = item.effect;
        
    }
    public void HideInfo()
    {
        infoPanel.alpha = 0;
    }
    public void FollowMouse()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 offset = new Vector3(10, -10, 0);
        infoPanelrect.position = mouse + offset;

    }
}
