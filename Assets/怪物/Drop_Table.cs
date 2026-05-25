using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Drop_item
{
    public Loot item;
    public int min_amount;
    public int max_amount;
    public int possibility;
}

public class Drop_Table : MonoBehaviour
{
    public Drop_item[] loots;
    System.Random rand = new System.Random();
    public void Drop_Loot(Transform position)
    {
        for(int i = 0;i<loots.Length;i++)
        {
            Drop_item cur = loots[i];

            if (rand.Next(100) > cur.possibility) continue;

            Vector2 randomOffset = Random.insideUnitCircle * 1;
            Debug.Log("掉落物品触发！");
            Loot new_loot = Instantiate(cur.item, position.position + new Vector3(randomOffset.x, randomOffset.y, 0)*2, position.rotation);

            if (new_loot != null)
            {
                // 随机偏移，避免堆叠
                int quantity = rand.Next(cur.min_amount,cur.max_amount+1);
                Debug.Log("掉落了" + quantity.ToString() + "个" + new_loot.item.name);
                new_loot.Initialize(quantity, cur.item.item);
                
            }
        }
    }
}
