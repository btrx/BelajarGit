using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Penghubung Ke Data")]
    [SerializeField] public PlayerData playerData;

    public float currentHP;
    public float moveSpeed;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        currentHP = playerData.maxHP;
    }
    
    
    void Update()
{
    if (playerInput == null) return;
    
    moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
    
    if (moveInput != Vector2.zero) Debug.Log("Input Terdeteksi: " + moveInput);

    float h = moveInput.x;
    float v = moveInput.y;

    if (playerData == null) return;
    transform.Translate(new Vector3(h, v, 0) * playerData.moveSpeed * Time.deltaTime);
}

    void OnCollisionStay2D(Collision2D collision)
{
    if (playerData != null && collision.gameObject.CompareTag("Wall"))
    {
        TakeDamage(playerData.damageTaken);
    }
}

 void TakeDamage(float dmg)
{
    currentHP -= dmg;
    Debug.Log("Player HP: " + currentHP);

    if (currentHP <= 0)
    {
        // Cek apakah GameManager sudah ada di Scene
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            Debug.LogError("Waduh! Kamu lupa pasang script GameManager di Hierarchy!");
        }
    }
}
}