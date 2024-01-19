using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class layer : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] float jumpForce = 20f;

    private bool isJumping = false;
    private bool isRunning = false;
    private Rigidbody2D rb;
    private Animator animator;
    SpriteRenderer spriteRenderer;
    public TextMeshProUGUI poin;
    int count;
    void Tong(int score){
        count +=score;
        poin.text = "Diem :"+ count;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger vao: " + other.gameObject.tag);

        if (other.gameObject.tag == "apple")
        {
            Destroy(other.gameObject);
            Tong(1);
        }
        else if (other.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(1);
        }
    }

    private void Start()
    {
        Tong(0);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Di chuyển ngang
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // Kiểm tra và cập nhật trạng thái Running
        if (moveX != 0 && !isRunning)
        {
            isRunning = true;
            animator.SetBool("isRuning", true);
        }
        else if (moveX == 0 && isRunning)
        {
            isRunning = false;
            animator.SetBool("isRuning", false);
        }
        if (moveX < 0)
        {
            spriteRenderer.flipX = true; // Quay sang trái
        }
        else if (moveX > 0)
        {
            spriteRenderer.flipX = false; // Quay sang phải
        }

        // Nhảy
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu đang va chạm với mặt đất
        if (collision.gameObject.CompareTag("san"))
        {
            isJumping = false;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Kiểm tra nếu không còn va chạm với mặt đất
        if (!isJumping && collision.gameObject.CompareTag("san"))
        {
            isRunning = false;
            animator.SetBool("isRuning", false);
        }
    }
}