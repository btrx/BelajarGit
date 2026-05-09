using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   // Patokan baru: Referensi ke Scriptable Object sesuai instruksi UTS
    public PlayerData data; 

    // Variabel tetap ada sesuai patokan awal kamu, tapi nilainya diambil dari 'data'
    public float currentHP; 
    public float speed;
    
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
        else
        {
            Debug.LogWarning("PlayerData Scriptable Object belum diassign! Pastikan untuk mengisi data pada Inspector.");
        }
    }
    
    
    void Update()
    {
        if (playerInput == null) return;
        
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);
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