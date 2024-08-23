using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Private Variables
    private Rigidbody2D rb;
    private Animator anim;
    private float moveX;

    //Public Variables
    public float speed;
    public int addJumps;
    public bool isGrounded;
    public float jumpForce;
    public int life;
    public TextMeshProUGUI lifeText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");  
        // Coloque a detecção de input aqui para garantir que seja registrada a cada frame
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            addJumps = 1;
            Jump();
        }
        else if (!isGrounded && Input.GetButtonDown("Jump") && addJumps > 0)
        {
            Jump();
            addJumps--;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        lifeText.text = life.ToString();
    }
    
    void FixedUpdate()
    {
        Move();
        
    }

    void Move()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        if(moveX > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(moveX < 0) //If the player is moving left then flip the sprite
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (moveX != 0)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetBool("isJump", true);
    }

    void Attack()
    {
        anim.Play("attack", -1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isJump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
