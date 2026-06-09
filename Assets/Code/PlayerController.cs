using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6;
    public float jumpForce = 8;
    public bool isGrounded = false;
    public int deaths = 0;
    public GameObject respawnPoint;
    public TextMeshProUGUI deathCountTxt;
    public int points = 0;
    public TextMeshProUGUI pointsTxt;

    public Rigidbody2D rb;
    public Animator animator;
    public bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead)
            return;

        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump();

        UpdateAnimator(moveInput);
    }

    void UpdateAnimator(float moveInput)
    {
        if (animator == null)
            return;

        animator.SetBool("caminando", isGrounded && Mathf.Abs(moveInput) > 0.01f);
        animator.SetBool("saltando", !isGrounded);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var tagCollision = col.transform.tag;

        if ((tagCollision == "Death" || tagCollision == "Enemy") && !isDead)
            Morir();

        if (tagCollision == "Coin")
        {
            points++;
            Destroy(col.gameObject);
            if (pointsTxt != null)
                pointsTxt.text = "PUNTOS " + points;
        }

        if (tagCollision == "EndGame")
            SceneManager.LoadScene("Juego");
    }

    void Morir()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        deaths++;
        if (deathCountTxt != null)
            deathCountTxt.text = "MUERTES " + deaths;

        if (animator != null)
        {
            animator.SetBool("caminando", false);
            animator.SetBool("saltando", false);
            animator.SetBool("muerto", true);
        }
    }

    public void ReiniciarPosicion()
    {
        if (!isDead)
            return;

        if (respawnPoint != null)
            transform.position = respawnPoint.transform.position;

        isDead = false;
        rb.linearVelocity = Vector2.zero;
        rb.simulated = true;

        if (animator != null)
        {
            animator.SetBool("muerto", false);
            animator.SetBool("caminando", false);
            animator.SetBool("saltando", false);
            animator.Play("jugador reposo", 0, 0f);
            animator.Update(0f);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.tag == "Floor")
            isGrounded = false;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.transform.tag == "Floor")
            isGrounded = true;
            
    }

    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        if (animator != null)
            animator.SetBool("saltando", true);
    }
}
