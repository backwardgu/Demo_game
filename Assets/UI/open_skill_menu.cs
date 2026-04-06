using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class open_skill_menu : MonoBehaviour
{
    public CanvasGroup canvas;
    private bool is_canva_open;
    private void Update()
    {
        if (Input.GetButtonDown("Skill"))
        {
            if (is_canva_open)
            {
                Time.timeScale = 1;
                canvas.alpha = 0;
                canvas.blocksRaycasts = false;
                is_canva_open = false;
            }

            else
            {
                Time.timeScale = 0;
                canvas.alpha = 1;
                canvas.blocksRaycasts = true;
                is_canva_open = true;
            }
        }
    }
}
