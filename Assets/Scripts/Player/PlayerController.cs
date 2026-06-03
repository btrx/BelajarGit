using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

{
    [SerializeField] private PlayerData playerData;
    public float currentHP;
    public float speed;
    private PlayerInput playerInput;
    private Vector2 moveInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerData == null)
        {
            Debug.LogError("PlayerData belum di-assign di Inspector!");
            return;
        }

        currentHP = playerData.maxHP;
        speed = playerData.moveSpeed;
    }
    
    
    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentState != GameState.Playing)
        return;
        
        if (playerInput != null)
        {
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 direction = new Vector3(moveInput.x, moveInput.y, 0);
        transform.Translate(direction * speed * Time.deltaTime);
        }
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
        if (currentHP <= 0 && GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        } 
    }
}