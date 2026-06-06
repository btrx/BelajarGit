using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Data")]
    public PlayerData playerData;

    public float currentHP;
    public float speed;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    // Attack variables
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    // Variabel input serangan
    private float attackInput;
    private float previousAttackInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        currentHP = playerData.maxHP;
        speed = playerData.moveSpeed;
    }

    void Update()
    {
        if (playerInput == null) return;

        // Input movement
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        // Input attack
        attackInput = playerInput.actions["Attack"].ReadValue<float>();

        float h = moveInput.x;
        float v = moveInput.y;

        // Gerakan player
        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);

        // Cek apakah tombol attack baru ditekan
        if (previousAttackInput == 0 && attackInput > 0)
        {
            Shoot();
        }

        // Simpan input sebelumnya
        previousAttackInput = attackInput;
    }

    void Shoot()
    {
        Debug.Log("Player is shooting!");

        if (bulletPrefab == null)
        {
            Debug.LogWarning("Bullet prefab not assigned!");
            return;
        }

        // Posisi spawn bullet
        Vector3 spawnPos = bulletSpawnPoint != null
            ? bulletSpawnPoint.position
            : transform.position;

        // Ambil posisi mouse di world
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);

        Vector3 mouseWorldPos =
            Camera.main.ScreenToWorldPoint(mouseScreenPos);

        mouseWorldPos.z = 0;

        // Hitung arah tembakan
        Vector3 shootDirection =
            (mouseWorldPos - spawnPos).normalized;

        Debug.Log(
            $"Spawn Pos: {spawnPos}, " +
            $"Mouse Pos: {mouseWorldPos}, " +
            $"Direction: {shootDirection}"
        );

        // Spawn bullet
        //GameObject bulletObj = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

        GameObject bulletObj =
    Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        

        if (bulletObj != null)
        {
            bulletObj.transform.position = spawnPos;
            bulletObj.transform.rotation = Quaternion.identity;
            bulletObj.SetActive(true);

            // Ambil script Bullet
            Bullet bullet = bulletObj.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.SetDirection(shootDirection);

                Debug.Log(
                    $"Bullet direction set to: {shootDirection}"
                );
            }
            else
            {
                Debug.LogError(
                    "Bullet component not found on prefab!"
                );
            }

            Debug.Log("Bullet spawned!");
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