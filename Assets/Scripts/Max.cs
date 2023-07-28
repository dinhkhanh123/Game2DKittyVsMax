using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Max : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public float jumpForce;
    public float speed;
    private Animator anim;
    private float translation;

    public LayerMask whatIsGround;
    public Transform groundPosition;
    public bool Grounded = true;
    

    private Rigidbody2D rb;
    private Kitty kitty;
    private bool allowJump = false;
    void Start()
    {
        kitty = GameObject.FindGameObjectWithTag("Player").GetComponent<Kitty>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        translation = 1;
     
    }

    // Update is called once per frame
    void Update()
    {
    

    }


    void FixedUpdate()
    {
        TurnPlayer();
        // Grounded = isGrounded();

        //translation = Input.GetAxisRaw("Horizontal");

        AI();

        // transform.Translate(new Vector3(translation, 0f, 0f)*Time.deltaTime*speed);
        rb.velocity = new Vector2(translation * Time.deltaTime * speed, rb.velocity.y);

        if (translation > 0 || translation < 0)
        {
            anim.SetFloat("speed", 1);
        }
        if (translation == 0)
        {
            anim.SetFloat("speed", 0);
        }

    }

    void TurnPlayer()
    {
        if (translation < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (translation > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    bool isGrounded()
    {
        if (Physics2D.OverlapCircle(groundPosition.position, 0.3f, whatIsGround))
        {
            return true;
        }
        return false;
    }

    void jump()
    {
        if (!isGrounded())
        {
            return;
        }
        Vector3 vel = rb.velocity;
        vel.y = 0;
        rb.velocity = vel;
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "jump")
        {
            allowJump = true;
            LeftRightDecision();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "jump")
        {
            allowJump = false;
            
        }
    }

    public void SwitchDirection()
    {
        if (translation == 1)
            translation = -1;
        else
            translation = 1;
    }

    void LeftRightDecision()
    {
        // kitty ben phai di chuyen ve phia kitty
        if (kitty.getXpos  > transform.position.x)
        {
            translation = 1f;
        }
        else if (kitty.getXpos  < transform.position.x)
        // kitty ben trai di chuyen ve phia kitty
        {
            translation = -1f;
        }
    }
    void AI()
    {
        
        //khi v? trí y c?a ng??i ch?i l?n h?n k? thù
        if (kitty.getYpos > transform.position.y && allowJump)
        {
            jump();
        }
//n?u k? thù ?ánh ??i t??ng nh?y
    }
}




