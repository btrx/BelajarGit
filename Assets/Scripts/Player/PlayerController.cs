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
            Debug.LogError("PlayerData belum di-assign ke PlayerController.");
            enabled = false;
            return;
        }

        currentHP = playerData.maxHP;
        speed = playerData.moveSpeed;
            
        var managers = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        Debug.Log("Jumlah GameManager: " + managers.Length);

        foreach (var manager in managers)
        {
            Debug.Log("GameManager instance: " + manager.name + " | scene: " + manager.gameObject.scene.name);
        }
    
    }

    void Update()
    {
        if (playerInput == null) return;
        if (GameManager.Instance != null && GameManager.Instance.CurrentState != GameState.Playing) return;

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (isDead) return;
        if (collision.gameObject.CompareTag("Wall") && playerData != null)
        {
            TakeDamage(playerData.wallDamage);
        }
    }

    void TakeDamage(float dmg)
    {
        if (isDead) return;

        currentHP -= dmg;

        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0)
        {
            isDead = true;
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
            else
            {
                Debug.LogError("GameManager instance not found!");
            }
        }
    }
}