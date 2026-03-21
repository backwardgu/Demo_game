using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevation_Exist : MonoBehaviour
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
        if (collision.gameObject.tag == "Player" && moutaincolliders[0].enabled == false)
        {
            foreach (Collider2D moutain in moutaincolliders)
            {
                moutain.enabled = true;
            }
            foreach (Collider2D bounder in boundercollider)
            {
                bounder.enabled = false;
            }
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }
}