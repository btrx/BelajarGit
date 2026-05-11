using UnityEngine;
using UnityEngine.InputSystem;
 
public class PlayerController : MonoBehaviour
{
    [Header("Data (Scriptable Object)")]
    [SerializeField] private PlayerData data;
 
    // Runtime state — tidak ada hardcode nilai
    private float currentHP;
    private float speed;
 
    private PlayerInput playerInput;
    private Vector2 moveInput;
 
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
 
        if (data != null)
        {
            currentHP = data.maxHP;
            speed = data.moveSpeed;
        }
        else
        {
            Debug.LogError("PlayerData ScriptableObject belum di-assign di Inspector!");
        }
    }
 
    private void Update()
    {
        // Hanya bergerak saat state Playing
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.currentState != GameState.Playing) return;
        if (playerInput == null) return;
 
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        transform.Translate(new Vector3(moveInput.x, moveInput.y, 0f) * speed * Time.deltaTime);
    }
 
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(0.1f);
        }
    }
 
    private void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        Debug.Log("Player HP: " + currentHP);
 
        if (currentHP <= 0f)
        {
            currentHP = 0f;
            GameManager.Instance.ChangeState(GameState.GameOver);
        }
    }
}