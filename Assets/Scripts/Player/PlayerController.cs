using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;

    private float currentHP;
    private float speed;
    private bool isDead = false;
    private PlayerInput playerInput;
    private Vector2 moveInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerData == null)
        {
            enabled = false;
            return;
        }

        currentHP = playerData.maxHP;
        speed = playerData.moveSpeed;
    }

    void Update()
    {
        if (playerInput == null) return;
        if (GameManager.Instance != null && GameManager.Instance.CurrentState != GameState.Playing) return;

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        transform.Translate(new Vector3(moveInput.x, moveInput.y, 0) * speed * Time.deltaTime);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (isDead) return;
        if (collision.gameObject.CompareTag("Wall") && playerData != null)
            TakeDamage(playerData.wallDamage);
    }

    void TakeDamage(float dmg)
    {
        if (isDead) return;
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            isDead = true;
            if (GameManager.Instance != null)
                GameManager.Instance.GameOver();
        }
    }
}