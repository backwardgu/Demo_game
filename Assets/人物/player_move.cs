using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
    public Rigidbody2D physics;
    public Animator animator;
    public int faceing;
    public bool is_knocked;
    public Player_combat combat;
    // Start is called before the first frame update
    void Start()
    {
        faceing = 1;
    }
    private void Update()
    {
        if(Input.GetButtonDown("Slash"))
        {
            combat.attack();
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
        animator.SetFloat("horizontal",Math.Abs(horizontal));
        animator.SetFloat("vertical", Math.Abs(vertical));

        //传参给刚体
        physics.velocity = new Vector2(horizontal,vertical) * StatusManager.Instance.speed;

        //传参给精灵
        if (horizontal > 0 && transform.localScale.x < 0 ||
           horizontal < 0 && transform.localScale.x > 0) fliping();
    }
    void fliping()
    {
        if (combat.is_attacking) return;
        faceing *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
    }
    public void  knock_back(Transform enemy,float hit_back,float hit_time)
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
//What the fuck?