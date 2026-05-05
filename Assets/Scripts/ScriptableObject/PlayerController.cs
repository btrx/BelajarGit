using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Slot ini harus diisi di Inspector Unity!
    public PlayerData data;

    private float currentHP;
    private PlayerInput playerInput;
    private Vector2 moveInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
        // Memastikan data tidak kosong
        if (data != null) 
        {
            currentHP = data.maxHP;
        }
        else 
        {
            Debug.LogError("OI FIN! Tarik file PlayerData ke slot 'Data' di Inspector Player!");
        }
    }

    void Update()
    {
        // Supaya gak error kalau PlayerInput atau Data kosong
        if (playerInput == null || data == null) return;

        // Membaca input
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        // Menggerakkan karakter
        transform.Translate(new Vector3(moveInput.x, moveInput.y, 0) 
            * data.moveSpeed * Time.deltaTime);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(10f * Time.deltaTime);
        }
    }

    void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            currentHP = 0;
            // Pastikan GameManager sudah di-setup dengan benar
            if (GameManager.Instance != null) 
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}