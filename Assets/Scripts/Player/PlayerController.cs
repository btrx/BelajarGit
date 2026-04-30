using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Penghubung Ke Data")]
    [SerializeField] private PlayerData PlayerData;

    private float currentHP;


    private PlayerInput playerInput;
    private Vector2 moveInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        currentHP = PlayerData.maxHP;
    }
    
    
    void Update()
{
    if (playerInput == null) return;
    
    moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
    
    if (moveInput != Vector2.zero) Debug.Log("Input Terdeteksi: " + moveInput);

    float h = moveInput.x;
    float v = moveInput.y;

    if (PlayerData == null) return;
    transform.Translate(new Vector3(h, v, 0) * PlayerData.moveSpeed * Time.deltaTime);
}

    void OnCollisionStay2D(Collision2D collision)
{
    if (PlayerData != null && collision.gameObject.CompareTag("Wall"))
    {
        TakeDamage(PlayerData.damageTaken);
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