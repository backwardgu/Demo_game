using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.Windows.Speech;
public enum Enemy_state
{
    IDLE,
    Chasing,
    Walking,
    Attacking,
    Attacking_up
}
public class Goblin_move : MonoBehaviour
{
    private Rigidbody2D rb;
    private int facing_direction;
    public float speed;
    public Animator animator;
    public Enemy_state enemy_State;
    bool is_attacking;
    bool is_knocked;
    //对象固有属性
    public Transform player;
    public Transform detect_point;
    public float Detect_range;
    public LayerMask player_layer;

    //检测周围
    public float attack_range;
    public float attack_cool_down;
    public float attack_timer;
    //攻击相关属性
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = 3;
        facing_direction = 1;
        attack_range = 2F;
        change_status(Enemy_state.IDLE);
        attack_cool_down = 2;
        Detect_range = 5F;
        is_knocked = false;

    }

    // Update is called once per frame
    void Update()
    {
        attack_timer -= Time.deltaTime;
        Check_player();
        switch (enemy_State)
        {
            case Enemy_state.Chasing:
                chase();
                break;
            case Enemy_state.Attacking:
                attacking();
                break;
        }
    }
    void chase()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
        if (rb.velocity.x < 0 && facing_direction > 0 ||
            rb.velocity.x > 0 && facing_direction < 0)
        {
            fliping();
        }
    }
    void attacking()
    {
        is_attacking = true;
        rb.velocity = Vector2.zero; 
        Vector2 direction = (player.position - transform.position).normalized;
        if (direction.y > 0 && direction.x < 0.7 && direction.x > -0.7)
        {
            animator.SetInteger("Attacking_mode", 2);
        }
        else if (direction.y < 0 && direction.x < 0.7 && direction.x > -0.7)
        {
            animator.SetInteger("Attacking_mode", 3);
        }
        else
        {
            animator.SetInteger("Attacking_mode", 1);
        }
    }
    private void Check_player()
    {
        if (is_attacking || is_knocked) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(detect_point.position,Detect_range,player_layer);
        if (hits.Length <= 0)
        {
            rb.velocity = Vector2.zero;
            change_status(Enemy_state.IDLE);
            return; 
        }
        float dis = Vector2.Distance(player.transform.position, transform.position);
        player = hits[0].transform;
        if ( dis < attack_range && attack_timer < 0)
        {
            change_status(Enemy_state.Attacking);
        }
        else if( dis < Detect_range && dis >= attack_range )
        {
            change_status(Enemy_state.Chasing);
        }
        else
        {
            change_status(Enemy_state.IDLE);
        }
    }
    public void fliping()
    {
        facing_direction *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    public void finish_attack()
    {
        is_attacking = false;
        attack_timer = attack_cool_down;
        change_status(Enemy_state.IDLE);
        animator.SetInteger("Attacking_mode", 0);
    }
    public void knock_back(Transform enemy, float hit_back, float hit_time)
    {
        is_knocked = true;
        Vector2 direction = (rb.transform.position - enemy.position).normalized;
        rb.velocity = direction * hit_back;
        StartCoroutine(KnockbackCounter(hit_time));
    }
    IEnumerator KnockbackCounter(float time)
    {
        yield return new WaitForSeconds(time);
        rb.velocity = Vector2.zero;
        is_knocked = false;
    }
    public void change_status(Enemy_state status)
    {
        enemy_State = status;
        switch(status)
        {
            case Enemy_state.IDLE:
                animator.SetBool("is_idle", true);
                animator.SetBool("is_chasing", false);
                speed = 0;
                rb.velocity = Vector2.zero;
                break;
            case Enemy_state.Chasing:
                animator.SetBool("is_idle", false);
                animator.SetBool("is_chasing", true);
                speed = 3;
                break;
            case Enemy_state.Attacking:
                animator.SetBool("is_idle", false);
                animator.SetBool("is_chasing", false);
                speed = 0;
                break;
        }
    }
}
