using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D RB;
    public Vector2 direction = Vector2.right;
    
    public float living_time;
    public float speed;
    public int damage;
    public float hit_back_strength;
    public float knock_back_time;

    public LayerMask enemy_layer;
    public LayerMask obstacle_layer;
    public SpriteRenderer sr;
    public Sprite buried_arrow;
    void Start()
    {
        hit_back_strength = 30f;
        knock_back_time = 0.1f;
        living_time = 5;
        damage = 1;
        speed = 20;
        RB.velocity = direction * speed;
        RotateArrow();
        Destroy(gameObject, living_time);
    }
    private void RotateArrow()
    {
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if((enemy_layer.value & (1 << collision.gameObject.layer)) >0)
        {
            collision.gameObject.GetComponent<Enemy_health>().Change_health(-damage);
            collision.gameObject.GetComponent<Goblin_move>().knock_back(transform,hit_back_strength,knock_back_time);
            Attach(collision.gameObject.transform);
        }
        else if((obstacle_layer.value& (1 << collision.gameObject.layer))>0)
        {
            Attach(collision.gameObject.transform);
        }
    }
    private void Attach(Transform target)
    {
        sr.sprite = buried_arrow;

        RB.velocity = Vector2.zero;
        speed = 0;
        RB.isKinematic = true;

        transform.SetParent(target);
    }
}
