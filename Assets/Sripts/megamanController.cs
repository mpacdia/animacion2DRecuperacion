using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class megamanController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D col;

    public Vector2 localSpeed;
    public float movX;

    public float characterSpeed;
    public float jumpHeight;

    public bool wannaJump;
    public bool onGround;
    public bool isJumping;
    public bool shooting = false;

    public List<AudioSource> randomAudio;
    public int index;
    public AudioSource currentAudio;

    public AudioSource yipi;
    public AudioSource waha;
    public AudioSource whoa;
    public AudioSource yahu;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        randomAudio = new List<AudioSource>();
        randomAudio.Add(yipi);
        randomAudio.Add(waha);
        randomAudio.Add(whoa);
        randomAudio.Add(yahu);

    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        localSpeed = Vector2.zero;
        onGround = true;
        isJumping = false;
        wannaJump = false;

        characterSpeed = 5f;
        jumpHeight = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        movX = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            wannaJump = true;
            index = Random.Range(0, randomAudio.Count);
            currentAudio = randomAudio[index];
            currentAudio.Play();
        }

        if (movX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movX > 0)
        {
            spriteRenderer.flipX = false;
        }

       if (Input.GetMouseButtonDown(0))
        {
            shooting = true;
        }

        animator.SetBool("shooting", shooting);
        animator.SetFloat("speed y", rb.velocity.y);
        animator.SetFloat("speed x", Mathf.Abs(rb.velocity.x));
    }

    public void FixedUpdate()
    {
        localSpeed = new Vector2(movX * characterSpeed, rb.velocity.y);

        rb.velocity = localSpeed;
        rb.velocity = localSpeed;

        if (wannaJump && onGround)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            wannaJump = false;
            onGround = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            onGround = true;
        }
    }

    public void stopShootingAnimation()
    {
        shooting = false;
    }
}
