using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevationentry : MonoBehaviour
{
    public Collider2D[] moutaincolliders;
    public Collider2D[] boundercollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D moutain in moutaincolliders)
            {
                moutain.enabled = false;
            }
            foreach (Collider2D bounder in boundercollider)
            {
                bounder.enabled = true;
            }
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }
    }
}
