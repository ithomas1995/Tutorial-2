using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;

    public Text winText;

    public Text loseText;
    public Text livesText;

    public AudioClip musicClipOne;

    public AudioClip musicClipBackground;

    public AudioClip musicClipTwo;

    public AudioClip musicClipThree;

    public AudioSource musicSource;

    private int scoreValue = 0;

    private int livesValue = 3;

    private bool facingRight = true;

    public float jumpForce;

    private float hozMovement;

    private float vertMovement;

    Animator anim;

private bool isOnGround;
public Transform groundcheck;
public float checkRadius;
public LayerMask allGround;



    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        winText.text = "";
        livesText.text = "Lives: " + livesValue.ToString();
        loseText.text = "";
        anim = GetComponent<Animator>();
        musicSource.clip = musicClipBackground;
        musicSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

         if (facingRight == false && hozMovement > 0)
        {
        Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
        Flip();
        }

        if (vertMovement > 0 && isOnGround == false)
        {
            anim.SetInteger("State", 2);
        }

        if (vertMovement <= 0 && isOnGround == true)
        {
            anim.SetInteger("State", 0);
            Debug.Log ("On Ground");
            
        }

        
    }



    void Update()
    {
         

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKey("right"))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKey("left"))
        {
            anim.SetInteger("State", 1);
        }
       
       if (Input.GetKey(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

    }

   void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            Destroy(collision.collider.gameObject);
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            
            if (scoreValue == 4)
            {
            transform.position = new Vector2(65.0f, -9.0f);
            livesValue = 3;
            livesText.text = "Lives: " +livesValue.ToString();
            }
            if (scoreValue == 8)
            {
                musicSource.clip = musicClipOne;
                musicSource.Play();
            }
        }

        if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            livesValue -= 1;
            livesText.text = "Lives: " + livesValue.ToString();
            if (livesValue == 0)
            {
                musicSource.clip = musicClipTwo;
                musicSource.Play();
            }
        }


        if(scoreValue == 8)
        {
            winText.text = "You win! -Ian Thomas";
            
        }

        if (livesValue == 0)
        {
            loseText.text = "Better luck next time! -Ian Thomas";
            Destroy (gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground" && isOnGround)
        {   
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }
}