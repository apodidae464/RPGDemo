using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    private int BossHP = 2;

    public Vector2 pointLeft;
    public Vector2 pointRight;
    public float moveSpeed;

    private Collider2D col;
    private Rigidbody2D rb;

    private bool isGoToLeft;
    private bool isGoToRight;

    private float sin = 1;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        isGoToLeft = true;
        isGoToRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGoToLeft)
        { 
            rb.velocity = new Vector2(-moveSpeed, sin);
            if(transform.position.x <= pointLeft.x)
            {
                isGoToLeft = false;
                isGoToRight = true;
            }
        }
        else
        {
            rb.velocity = new Vector2(moveSpeed, sin);
            if (transform.position.x >= pointRight.x)
            {
                isGoToLeft = true;
                isGoToRight = false;
            }
        }

    }

    private void LateUpdate()
    {
        sin = -sin;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            BossHP--;
            if (BossHP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
