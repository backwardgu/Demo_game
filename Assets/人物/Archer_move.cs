using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_move : MonoBehaviour
{
    public Rigidbody2D physics;
    public Animator animator;
    public int faceing;
    public SpriteRenderer sr;
    public bool is_knocked;
    public Player_bow bow;
    // Start is called before the first frame update
    void Start()
    {
        faceing = 1;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Slash"))
        {
            bow.attack();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (is_knocked)
        {
            //传参给动画器
            animator.SetFloat("horizontal", 0);
            animator.SetFloat("vertical", 0);

            return;
        }
        //获取输入情况
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //传参给动画器
        animator.SetFloat("horizontal", Math.Abs(horizontal));
        animator.SetFloat("vertical", Math.Abs(vertical));

        //传参给刚体
        physics.velocity = new Vector2(horizontal, vertical) * StatusManager.Instance.speed;

        //传参给精灵
        if (horizontal > 0 && faceing < 0 ||
           horizontal < 0 && faceing > 0) fliping();
    }
    public void fliping()
    {
        if (!bow.can_change()) return;
        faceing *= -1;
        bow.facing_direction *= -1;
        sr.flipX = (faceing == -1);
    }
    public void knock_back(Transform enemy, float hit_back, float hit_time)
    {
        is_knocked = true;
        Vector2 direction = (physics.transform.position - enemy.position).normalized;
        physics.velocity = direction * hit_back;
        StartCoroutine(KnockbackCounter(hit_time));
    }
    IEnumerator KnockbackCounter(float time)
    {
        yield return new WaitForSeconds(time);
        physics.velocity = Vector2.zero;
        is_knocked = false;
    }
}
