using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Kitty : MonoBehaviour
{
  
    // Start is called before the first frame update
    public float jumpForce;
    public float speed ;
    private Animator anim;
    private float translation;

    public GameObject particle;
    public LayerMask whatIsGround;
    public Transform groundPosition;
    public bool Grounded = true;

    public TextMeshPro ScoreText;

    private bool isDeath = false;   

    private Rigidbody2D rb;

    public float getXpos
    {
        get { return rb.position.x; }
    }

    public float getYpos
    {
        get { return rb.position.y; }
    }

    public bool movingRight
    {
        get
        {
            if (translation == 1)
            { return true; }

            return false;
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>(); 
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump();
        }
    }


    void FixedUpdate()
    {
        TurnPlayer();
       // Grounded = isGrounded();

        translation = Input.GetAxisRaw("Horizontal");
       // transform.Translate(new Vector3(translation, 0f, 0f)*Time.deltaTime*speed);
        rb.velocity = new Vector2(translation * Time.deltaTime * speed, rb.velocity.y);

        if (translation >0 || translation < 0)
        {
            anim.SetFloat("speed", 1);
        }
        if (translation ==  0)
        {
            anim.SetFloat("speed", 0);
        }

    }

    void TurnPlayer()
    {
        if(translation < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        if(translation > 0)
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

        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "coin")
        {
           // ScoreText.text = (int.Parse(ScoreText.text) + 5).ToString();
          GameObject p =  Instantiate(particle, col.transform.position, particle.transform.rotation);
            Destroy(p,0.5f);
            Destroy(col.gameObject);
        }

        if(col.tag == "home")
        {
            if(transform.childCount > 1)
            {
                GameObject chicken = transform.GetChild(1).gameObject;
                chicken.GetComponent<Chicken>().follow = false;
                chicken.transform.parent = null;
                chicken.GetComponent<Collider2D>().enabled = false;
                chicken.GetComponent<ChickenRun>().enabled = true;

                StartCoroutine(ChickenDestroy(chicken));
            }
        }
    }

    IEnumerator ChickenDestroy(GameObject chicken)
    {
        yield return new WaitForSeconds(1);
        chicken.SetActive(false);

        int ChickenCount = GameObject.FindGameObjectsWithTag("Chicken").Length;

        if(ChickenCount == 0)
        {
            
            UIhandel.instance.ShowLevelDialog("Level Cleared.");    
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Max")
        {
            if (isDeath)
            {
                return;
            }
            anim.SetTrigger("death");
            isDeath= true;
           // UIhandel.instance.ShowLevelDialog("Level Failed.");
        }
            
    }
}



//?o?n mã này s? d?ng engine game Unity ?? di chuy?n m?t ??i t??ng trong m?t c?nh 3D.

//  transform là m?t thành ph?n c?a m?t ??i t??ng game ??i di?n cho v? trí, xoay và t? l?.

//Translate là m?t ph??ng th?c di chuy?n ??i t??ng game m?t kho?ng cách nh?t ??nh theo m?t h??ng nh?t ??nh.

//new Vector3 t?o m?t vector 3D m?i.Giá tr? Input.GetAxisRaw("Horizontal") ???c s? d?ng cho tr?c X, có ngh?a là ??i t??ng s? di chuy?n theo chi?u ngang. Giá tr? này ???c xác ??nh b?i ??u vào c?a ng??i ch?i trên bàn phím ho?c b? ?i?u khi?n.

//Giá tr? 0f cho tr?c Y và Z có ngh?a là ??i t??ng s? không di chuy?n lên xu?ng ho?c ?i lùi.

//T?ng th?, ?o?n mã này di chuy?n ??i t??ng game theo chi?u ngang d?a trên ??u vào c?a ng??i ch?i trên tr?c X.
//Time.deltaTime là th?i gian gi?a hai khung hình (frame) liên ti?p trong game. Khi s? d?ng Time.deltaTime, ?o?n mã s? di chuy?n ??i t??ng game v?i cùng t?c ?? trên các máy tính khác nhau.

//speed là m?t giá tr? s? th?c (float) ??i di?n cho t?c ?? di chuy?n c?a ??i t??ng game.