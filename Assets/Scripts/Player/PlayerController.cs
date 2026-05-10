using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public PlayerData data; 
    
    private PlayerInput playerInput;
    private Vector2 moveInput;
    private float currentHP;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
        if (data != null) 
        {
            currentHP = data.maxHP;
        }
    }

    void Update()
    {
        if (playerInput == null || data == null) return;
        
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        
        Vector3 moveDirection = new Vector3(moveInput.x, moveInput.y, 0);
        transform.Translate(moveDirection * data.moveSpeed * Time.deltaTime);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
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
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}