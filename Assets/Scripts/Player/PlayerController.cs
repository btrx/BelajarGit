using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Data")]
    public PlayerData playerData;

    private PlayerInput playerInput;
    private Vector2 moveInput;
    private float currentHP;
    private float speed;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerData == null)
        {
            Debug.LogError("PlayerData belum di-assign di Inspector.");
            currentHP = 100f;
            speed = 5f;
        }
        else
        {
            currentHP = playerData.maxHP;
            speed = playerData.moveSpeed;
        }
    }

    private void Update()
    {
        if (playerInput == null) return;

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f) * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (playerData == null) return;
        if (collision.gameObject.CompareTag("Wall"))
        {
            ApplyDamage(playerData.wallDamagePerSecond * Time.fixedDeltaTime);
        }
    }

    private void ApplyDamage(float damage)
    {
        currentHP -= damage;
        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0f)
        {
            currentHP = 0f;
            GameManager.Instance?.GameOver();
        }
    }
} 