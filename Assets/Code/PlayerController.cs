using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public Rigidbody2D rb;
    public float moveSpeed = 6;
    public float jumpForce = 5;
    public bool isGrounded = false;
    public int deaths = 0;
    public GameObject RespawnPoint;
    public TextMeshProUGUI deathCountTxt;
    public int points = 0;
    public TextMeshProUGUI pointsTxt;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player Movement
        float moveInput = Input.GetAxis("Horizontal");

        // Movement force
        var movement = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Apply force to player
        rb.linearVelocity = movement;

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
         // Death
        if (tagCollision == "Death")
        {
            deaths++;
            gameObject.transform.position = RespawnPoint.transform.position;
            deathCountTxt.text = "MUERTES " + deaths;
        }
        
        // Points
        if (tagCollision == "Coin")
        {
            points++;
            pointsTxt.text = "PUNTOS " + points;
            Destroy(col.gameObject);
        }

        // Finish
        if (tagCollision == "Finish")
        {
            SceneManager.LoadScene("Game");
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        var tagCollision = col.transform.tag;
        if (tagCollision == "Floor")
        {
            isGrounded = false;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        var tagCollision = col.transform.tag;
        if (tagCollision == "Floor")
        {
            isGrounded = true;
        }
    }
        
    public void Jump()
    {
        var jump = new Vector2(rb.linearVelocity.x, jumpForce);
        rb.linearVelocity = jump;
    }
}
