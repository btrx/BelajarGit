using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Data")]
    public PlayerData playerData;

    private float currentHP;
    private float speed;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        currentHP = playerData.maxHP;
        speed = playerData.moveSpeed;
    }

    void Update()
    {
        if (playerInput == null) return;

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(Time.deltaTime);
        }
    }

    void TakeDamage(float dmg)
    {
        if (currentHP <= 0)
        {
            return;
        }

        currentHP -= dmg;

        if (currentHP <= 0)
        {
            currentHP = 0;

            Debug.Log("Player HP : " + currentHP);
            Debug.Log("Game Over");

            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }

            return;
        }

        Debug.Log("Player HP : " + currentHP);
    }
}