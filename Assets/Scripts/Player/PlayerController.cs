using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float currentHP;
    public PlayerData data;
    private PlayerInput playerInput;
    private Vector2 moveInput;

    void Start()
    {

        playerInput = GetComponent<PlayerInput>();
        Debug.Log("Health :" + data.maxHP);
        Debug.Log("Speed :" + data.moveSpeed);
        currentHP = data.maxHP;
    }
    
    
    void Update()
    {
        if (playerInput == null) return;
        
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * data.moveSpeed * Time.deltaTime);
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

        if (data.maxHP <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}