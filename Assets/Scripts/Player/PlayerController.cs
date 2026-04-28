using UnityEngine;
using UnityEngine.InputSystem; // WAJIB ADA ini supaya PlayerInput tidak error

public class PlayerController : MonoBehaviour
{
    // Slot untuk memasukkan file MyPlayerData dari Unity
    public PlayerData data; 

    private float currentHP;
    private PlayerInput playerInput;
    private Vector2 moveInput;

    void Start()
    {
        // Mengambil HP dari Scriptable Object
        if (data != null) 
        {
            currentHP = data.maxHP;
        }

        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        // Cek apakah input dan data sudah terpasang
        if (playerInput == null || data == null) return;
        
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        
        // Gerakkan player menggunakan speed dari Scriptable Object
        transform.Translate(new Vector3(moveInput.x, moveInput.y, 0) * data.moveSpeed * Time.deltaTime);
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
            Debug.Log("Game Over!");
            if (GameManager.Instance != null) {
                GameManager.Instance.GameOver();
            }
        }
    }
}