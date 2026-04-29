using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private float currentHP;
    private PlayerInput playerInput;
    private Vector2 moveInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerData == null)
        {
            Debug.LogError("PlayerData belum di-assign!");
            return;
        }

        currentHP = playerData.maxHP;
    }

    void Update()
    {
        if (playerInput == null) return;
        
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * playerData.moveSpeed * Time.deltaTime);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(playerData.damagePerSecond * Time.deltaTime);
        }
    }

    void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        currentHP = Mathf.Max(0, currentHP);

        Debug.Log("HP: " + currentHP);

        if (currentHP <= 0)
        {
            Debug.Log("=== GAME OVER ===");
            GameManager.Instance.GameOver();
        }
    }
}