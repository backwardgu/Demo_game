using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use_item : MonoBehaviour
{
    public void use(Item_template item)
    {
        if (item.max_health != 0)
            StatusManager.Instance.maxHealth += item.max_health;

        if (item.current_health != 0)
            StatusManager.Instance.currentHealth += item.current_health;

        if (item.speed != 0)
            StatusManager.Instance.speed += item.speed;

        if (item.max_health != 0)
            StatusManager.Instance.maxHealth += item.max_health;

        if (item.damage != 0)
            StatusManager.Instance.damage += item.damage;

        if (item.existing_time > 0)
            StartCoroutine(Effect_Timer(item,item.existing_time));
    }
    IEnumerator Effect_Timer(Item_template item,float time)
    {
        yield return new WaitForSeconds(time);

        if (item.max_health != 0)
            StatusManager.Instance.maxHealth -= item.max_health;

        if (item.speed != 0)
            StatusManager.Instance.speed -= item.speed;

        if (item.max_health != 0)
            StatusManager.Instance.maxHealth -= item.max_health;

        if (item.damage != 0)
            StatusManager.Instance.damage -= item.damage;

    }
}
