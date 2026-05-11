using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData data; 
    
    public float currentHP;
    private float speed;
    private PlayerInput playerInput;
    private Vector2 moveInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
        if (data != null)
        {
            currentHP = data.maxHP;
            speed = data.moveSpeed;
        }
    }
    
    void Update()
    {
    if (GameManager.Instance == null || GameManager.Instance.currentState != GameState.Playing) return;
    
    if (playerInput == null) return;
    
    moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
    transform.Translate(new Vector3(moveInput.x, moveInput.y, 0) * speed * Time.deltaTime);
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
        GameManager.Instance.GameOver(); 
    }
    }
}