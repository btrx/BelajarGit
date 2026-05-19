using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private float currentHP;
    private float speed;
    private PlayerInput playerInput;
    private Vector2 moveInput;
    private float attackInput;
    private float previousAttackInput;
    // variabel untuk menentukan posisi spawn peluru
    public Transform bulletSpawnPoint;
    // Variabel untuk menyimpan input serangan sebelumnya
    public GameObject bulletPrefab;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerData != null)
        {
            currentHP = playerData.maxHP;
            speed = playerData.moveSpeed;
        }
        else
        {
            Debug.LogError("PlayerData does not exist, please assign it first!");
        }
    }

    void Update()
    {
        if (playerInput == null) return;

        if (GameManager.Instance.currentState == GameState.Paused) return;

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        //baca input serangan
        attackInput = playerInput.actions["Attack"].ReadValue<float>();
        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);


        // Ini untuk ngecek apakah tombol serang baru saja ditekan
        if (previousAttackInput == 0 && attackInput > 0)
        {
            Shoot();
        }

        previousAttackInput = attackInput;
    }

    void Shoot()
    {
        Debug.Log("nyerang");
        Debug.Log("Player is shooting!");

        if (bulletPrefab == null)
        {
            Debug.LogWarning("Bullet prefab not assigned!");
            return;
        }

        // Determine spawn position
        Vector3 spawnPos = bulletSpawnPoint != null ? bulletSpawnPoint.position : transform.position;

        // Get mouse position in world space for 2D
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0; // Ensure Z is 0 for 2D

        // Calculate direction from player to mouse
        Vector3 shootDirection = (mouseWorldPos - spawnPos).normalized;

        Debug.Log($"Spawn Pos: {spawnPos}, Mouse World Pos: {mouseWorldPos}, Direction: {shootDirection}");

        // Instantiate bullet
        // GameObject bulletObj = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

        // Panggil objek dari Pool
        GameObject bulletObj = PooledObjects.Instance.GetPooledObject();

        if (bulletObj != null)
        {
            // Atur posisi dan rotasi peluru
            bulletObj.transform.position = spawnPos;
            bulletObj.transform.rotation = Quaternion.identity;

            // Aktifkan peluru
            bulletObj.SetActive(true);

            // Set arah peluru (Logika aslimu tetap dipertahankan)
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.SetDirection(shootDirection);
                Debug.Log($"Bullet direction set to: {shootDirection}");
            }
            else
            {
                Debug.LogError("Bullet component not found on prefab!");
            }
        }



        //     // Set bullet direction
        //     Bullet bullet = bulletObj.GetComponent<Bullet>();
        //     if (bullet != null)
        //     {
        //         bullet.SetDirection(shootDirection);
        //         Debug.Log($"Bullet direction set to: {shootDirection}");
        //     }
        //     else
        //     {
        //         Debug.LogError("Bullet component not found on prefab!");
        //     }

        //     Debug.Log("Bullet spawned!");
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(1f);
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