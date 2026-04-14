using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Player_bow : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public Transform launch_point;      // 必须是弓的子物体，位于箭的发射点
    public GameObject arrowPrefab;
    public Archer_move archer;

    public Transform detect_point;      // 用于检测敌人的点
    public float detect_range;
    public LayerMask enemy_layer;

    public int facing_direction = 1;
    public bool is_aiming;
    public bool is_shooting;
    public float shoot_cool = 0.5f;
    public float shoot_timer = 0;

    public Vector2 aimDirection = Vector2.right;
    private Transform currentTarget;    // 缓存当前目标，避免每帧 FindGameObjectWithTag


    void Update()
    {
        shoot_timer -= Time.deltaTime;
        HandleAiming();
        change_facing();
    }

    public void attack()
    {
        if (shoot_timer > 0 || is_shooting) return;

        is_shooting = true;
        shoot_timer = shoot_cool;
        animator.SetBool("shooting", true);
    }

    // 由 Animation Event 调用
    public void Shoot()
    {
        if (arrowPrefab == null || launch_point == null) return;

        // 关键修正：箭的旋转应该与弓的朝向一致
        // 使用 launch_point.rotation 而不是 Quaternion.identity
        Arrow arrow = Instantiate(arrowPrefab, launch_point.position, launch_point.rotation).GetComponent<Arrow>();
        if (arrow != null)
        {
            // 方向应该从弓的朝向来，而不是依赖 aimDirection
            arrow.direction = aimDirection;  // launch_point.right 是弓的右方向
        }
    }

    private void HandleAiming()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detect_point.position, detect_range, enemy_layer);

        if (hits.Length <= 0)
        {
            is_aiming = false;
            currentTarget = null;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            return;
        }

        is_aiming = true;
        currentTarget = FindClosestEnemyTransform(hits);
        if (currentTarget == null) return;

        // 计算方向
        Vector2 direction = (currentTarget.position - transform.position).normalized;
        aimDirection = direction;

        // 计算角度（不限制范围）
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 应用旋转
        transform.localRotation = Quaternion.Euler(0, 0, angle);

    }

    private Transform FindClosestEnemyTransform(Collider2D[] hits)
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;
        Vector2 currentPos = transform.position;

        foreach (Collider2D enemy in hits)
        {
            float distance = Vector2.Distance(currentPos, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }
        return closest;
    }

    public void EndShooting()
    {
        is_shooting = false;
        animator.SetBool("shooting", false);
    }

    private void OnDrawGizmosSelected()
    {
        if (detect_point != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(detect_point.position, detect_range);
        }
    }
    public bool can_change()
    {
        if (!is_aiming)
        {
            return true;
        }

        if (is_shooting) return false;

        if(aimDirection.x > 0 && facing_direction > 0)
        {
            return false;
        }

        else if(aimDirection.x < 0 && facing_direction < 0) 
        {
            return false;
        }
        return true;
    }
    public void change_facing()
    {
        if (!is_aiming) return;
        if (aimDirection.x > 0 && facing_direction < 0)
        {
            archer.fliping();
        }

        else if (aimDirection.x < 0 && facing_direction > 0)
        {
            archer.fliping();
        }
    }
}