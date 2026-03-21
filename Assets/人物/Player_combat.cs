using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum Attacking_mode
{
    Front1,
    Front2,
    Up,
    Down

}
public class Player_combat : MonoBehaviour
{
    public int damage;
    public float hit_range;
    public int hit_back;

    public float attack_timer;
    public float attck_cool;
    public bool is_attacking;
    public Animator animator;
    public Attacking_mode cur_attack;

    public LayerMask enemy_layer;

    public Transform attack_point_front1;
    public Transform attack_point_front2;
    public void Start()
    {
        attck_cool = 0.2F;
        is_attacking = false;
        hit_range = 0.45F;
        damage = 1;
        hit_back = 20;
    }
    public void Update()
    {
        attack_timer -= Time.deltaTime;
    }
    public void attack()
    {
        if (attack_timer > 0 || is_attacking) return;

        is_attacking = true;
        if (attack_timer < -2) cur_attack = Attacking_mode.Front2;


        if (cur_attack == Attacking_mode.Front2)
        {
            cur_attack = Attacking_mode.Front1;
            animator.SetInteger("attacking_mode", 1);
        }
        else
        {
            cur_attack = Attacking_mode.Front2;
            animator.SetInteger("attacking_mode", 2);
        }
    }
    public void FinishAttack()
    {
        attack_timer = attck_cool;
        is_attacking = false;
        animator.SetInteger("attacking_mode", 0);
    }
    public void slash(Attacking_mode attack_pattern)
    {
        Collider2D[] hits;
        Transform attack_point = attack_point_front1;
        switch (attack_pattern)
        {
            case Attacking_mode.Front1:

                Debug.Log("第一段攻击");
                break;
            case Attacking_mode.Front2:
                Debug.Log("第二段攻击");
                attack_point = attack_point_front2;
                break;
        }
        hits = Physics2D.OverlapCircleAll(attack_point.position, hit_range, enemy_layer);
        if (hits.Length > 0)
        {
            Debug.Log("命中怪物！");
            hits[0].GetComponent<Enemy_health>().Change_health(-damage);
            hits[0].GetComponent<Goblin_move>().knock_back(attack_point, hit_back, 0.25F);
        }
    }
}
