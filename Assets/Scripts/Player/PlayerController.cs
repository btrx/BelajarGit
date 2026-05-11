using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float currentHP = 100f;
    public float maxHP = 100f;
    public float speed = 5f;

    [Header("Damage Settings")]
    public float wallDamage = 5f;
    public float damageCooldown = 1f;

    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private float damageTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        // Stop movement saat pause atau game over
        if (GameManager.Instance.currentState != GameState.Playing)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // Ambil input movement
        if (playerInput != null)
        {
            moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        }
    }

    void FixedUpdate()
    {
        // Gerakan player
        rb.linearVelocity = moveInput * speed;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= damageCooldown)
            {
                TakeDamage(wallDamage);
                damageTimer = 0f;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            damageTimer = 0f;
        }
    }

    void TakeDamage(float dmg)
    {
        currentHP -= dmg;

        // Batas minimum HP
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);

        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}