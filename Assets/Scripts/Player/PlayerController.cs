using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerController : MonoBehaviour
{
    public PlayerData data; 

    private float currentHP;
    private PlayerInput playerInput;
    private Vector2 moveInput;

    void Start()
    {
        if (data != null) 
        {
            currentHP = data.maxHP;
        }
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        // Cek apakah game sedang berhenti
        if (Time.timeScale == 0f) return; 

        if (playerInput == null || data == null) return;
        
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        
        transform.Translate(new Vector3(moveInput.x, moveInput.y, 0) * data.moveSpeed * Time.deltaTime);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Pastikan tag "Wall" sudah dibuat di Unity
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(0.1f);
        }
    }

    void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0)
        {
            // Panggil fungsi GameOver di GameManager
            if (GameManager.Instance != null) 
            {
                GameManager.Instance.GameOver();
            }
            else 
            {
                Debug.LogWarning("GameManager belum ada di Scene!");
            }
        }
    }
}