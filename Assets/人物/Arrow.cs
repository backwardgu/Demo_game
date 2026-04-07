using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D RB;
    public Vector2 direction = Vector2.right;
    public float living_time;
    public float speed;
    void Start()
    {
        living_time = 5;
        speed = 20;
        RB.velocity = direction * speed;
        Destroy(gameObject, living_time);
    }
}
