using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Direction
{
    Right,
    Left
}

public class Player : MonoBehaviour
{
    private Direction direction;
    [SerializeField] private float speed = 5f;
    public GameObject Panel;
    [SerializeField] private Score scoreScript;
    [SerializeField] private int coins;
    
    public float jumpForce = 7f;
    Rigidbody2D rb;

    AudioSource jumpSound;
    private Animator anim;
  
    void Start()
    {
        direction = Direction.Right;
        rb = GetComponent<Rigidbody2D>();
        jumpSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        switch (direction)
        {
            case Direction.Left:
                transform.localScale = new Vector3(-0.2954769f, 0.2954769f, 0f);
                transform.position += Vector3.left * speed * Time.deltaTime;
                break;
            case Direction.Right:
                transform.localScale = new Vector3(0.2954769f, 0.2954769f, 0f);
                transform.position += Vector3.right * speed * Time.deltaTime;
                break;
        }

        rb.velocity = new Vector2(0, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            rb.velocity = new Vector2(0, jumpForce);
            jumpSound.Play();
            anim.SetTrigger("Jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("RightWall"))
        {
            direction = Direction.Left;

        }
        if (collision.collider.CompareTag("LeftWall"))
        {
            direction = Direction.Right;
        }

        if (collision.collider.CompareTag("Enemy"))
        {
            GetComponent<Player>().enabled = false;

            Panel.SetActive(true);
            int lastRunScore = int.Parse(scoreScript.scoreText.text.ToString());
            PlayerPrefs.SetInt("lastRunScore", lastRunScore);
        }
    }
}
