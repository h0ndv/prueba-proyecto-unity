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
    public GameObject respawnPoint;
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
        var tagCollision = col.transform.tag;

        // Death
        if (tagCollision == "Death" || tagCollision == "Enemy")
        {
            gameObject.transform.position = respawnPoint.transform.position;
            deaths++;
            if (deathCountTxt != null)
                deathCountTxt.text = "MUERTES " + deaths;
        }
        
        // Points
        if (tagCollision == "Coin")
        {
            points++;
            Destroy(col.gameObject);
            if (pointsTxt != null)
                pointsTxt.text = "PUNTOS " + points;
        }

        // Finish
        if (tagCollision == "EndGame")
        {
            SceneManager.LoadScene("SampleScene");
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
