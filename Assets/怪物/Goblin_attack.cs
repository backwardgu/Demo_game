using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum attacking_mode
{
    Front,
    Up,
    Down
}
public class Goblin_attack : MonoBehaviour
{
    public Transform attack_point_front;
    public Transform attack_point_up;
    public Transform attack_point_down;
    public int damage = 1;
    public float hit_back = 20;
    public float hit_range = 0.75F;
    public LayerMask playerLayer;

    public void attack(attacking_mode attack_pattern)
    {
        Collider2D[] hits;
        Transform attack_point = attack_point_front;
        switch (attack_pattern)
        {
            case attacking_mode.Front:

                Debug.Log("尝试攻击");
                break;
            case attacking_mode.Up:
                Debug.Log("尝试向上攻击");
                attack_point = attack_point_up;
                break;
            case attacking_mode.Down:
                Debug.Log("尝试向下攻击");
                attack_point = attack_point_down;
                break;
        }
        hits = Physics2D.OverlapCircleAll(attack_point.position, hit_range, playerLayer);
        if (hits.Length > 0)
        {
            Debug.Log("命中玩家！");
            hits[0].GetComponent<Player_health>().Change_health(-damage);
            hits[0].GetComponent<player_move>().knock_back(attack_point,hit_back,0.25F);
        }
    }
}
