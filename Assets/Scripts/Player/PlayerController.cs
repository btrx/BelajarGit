using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // [POIN 20] Slot untuk narik file PlayerData.asset dari Project ke Inspector
    public PlayerData data;

    private float currentHP;
    private float speed;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        // Mengambil data dari Scriptable Object agar tidak 'hardcode'
        if (data != null)
        {
            currentHP = data.maxHP;      // Pakai 'maxHP' sesuai file PlayerData kamu
            speed = data.moveSpeed;    // Pakai 'moveSpeed' sesuai file PlayerData kamu
        }
        else
        {
            Debug.LogError("Nan, file PlayerData belum kamu masukin ke slot 'Data' di Inspector Player!");
        }
    }

    void Update()
    {
        if (playerInput == null) return;

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        float h = moveInput.x;
        float v = moveInput.y;

        // Sekarang 'speed' di sini nilainya otomatis ngikutin PlayerData
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
            // Poin 20: Memanggil sistem GameOver saat darah habis
            GameManager.Instance.GameOver();
        }
    }
}