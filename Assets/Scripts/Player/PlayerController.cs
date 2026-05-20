using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;
    public float currentHP;

    public GameObject bulletPrefab;
    // public float speed;
    private PlayerInput playerInput;
    private Vector2 moveInput;

    private float attackInput;
    private float previousAttackInput; // Variabel untuk menyimpan input serangan sebelumnya

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerData == null)
        {
            Debug.LogError("PlayerData belum di-assign!");
        }

        if (playerInput == null)
        {
            Debug.LogError("PlayerInput tidak ditemukan!");
        }

        currentHP = playerData.maxHP;
    }
    
    
    void Update()
    {
        if (playerInput == null) return;

        if (GameManager.Instance.currentState != GameState.Playing) return;
        
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        Debug.Log(moveInput);
        // Baca input serangan
        attackInput = playerInput.actions["Attack"].ReadValue<float>();

        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * playerData.moveSpeed * Time.deltaTime);

        // Ini untuk mengecheck apakah tombol ditekan atau tidak
        if (attackInput > 0)
        {
            Shoot();
        }

        previousAttackInput = attackInput; // Simpan input serangan saat ini untuk periksa pada frame berikutnya
    }

    void Shoot()
    {
        Debug.Log("Player shoots!");
        // Implement shooting logic here (e.g., instantiate bullet, play animation)

        if (bulletPrefab == null)
        {
            Debug.LogWarning("Bullet prefab not assigned!");
            return;
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
        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}