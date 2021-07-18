using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public static PlayerController instance;

    public float moveSpeed;
    public string areaTransitionName;
    protected Rigidbody2D rb;
    protected Animator anim;

    public bool canMove = true;
    void Start()
    {
      
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        }
        else
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * 0;
        }
        anim.SetFloat("MoveX", rb.velocity.x);
        anim.SetFloat("MoveY", rb.velocity.y);

        if(Input.GetAxisRaw("Horizontal") == 1 
            || Input.GetAxisRaw("Horizontal") == -1 
            || Input.GetAxisRaw("Vertical") == 1 
            || Input.GetAxisRaw("Vertical") == -1)
        {
            anim.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Spider")
        {
            if (GameManager.instance)
                  GameManager.instance.playersStats.currentHP -= 2;
        }
        if (collision.tag == "Skeleton")
        {
            if (GameManager.instance)
                GameManager.instance.playersStats.currentHP -= 5;
        }
        if (collision.tag == "Dragon")
        {
            if (GameManager.instance)
                GameManager.instance.playersStats.currentHP -= 10;
        }
        if(GameManager.instance != null && GameManager.instance.playersStats.currentHP <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }
}
