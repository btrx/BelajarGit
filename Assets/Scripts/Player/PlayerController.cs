using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerData data;

    public float currentHP;
    public float speed;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    private float attackInput;
    private float previousAttackInput;

    public GameObject bulletPrefab;

    // Posisi spawn bullet
    public Transform bulletSpawnPoint;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        currentHP = data.maxHP;
        speed = data.moveSpeed;
    }

    void Update()
    {
        if (playerInput == null) return;

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        // Baca input serangan
        attackInput = playerInput.actions["attack"].ReadValue<float>();

        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);

        // Cek tombol attack baru ditekan
        if (previousAttackInput == 0 && attackInput > 0)
        {
            shoot();
        }

        previousAttackInput = attackInput;
    }

    void shoot()
    {
        Debug.Log("Player is shooting!");

        if (bulletPrefab == null)
        {
            Debug.LogWarning("Bullet prefab not assigned!");
            return;
        }

        // Posisi spawn
        Vector3 spawnPos = bulletSpawnPoint != null
            ? bulletSpawnPoint.position
            : transform.position;

        // Ambil posisi mouse
        Vector3 mouseScreenPos = Input.mousePosition;

        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);

        Vector3 mouseWorldPos =
            Camera.main.ScreenToWorldPoint(mouseScreenPos);

        mouseWorldPos.z = 0;

        // Arah tembakan
        Vector3 shootDirection =
            (mouseWorldPos - spawnPos).normalized;

        Debug.Log($"Spawn Pos: {spawnPos}");

        // =========================
        // OBJECT POOLING
        // =========================

        GameObject bulletObj =
            PooledObjects.Instance.GetPooledObject();

        if (bulletObj != null)
        {
            // Set posisi bullet
            bulletObj.transform.position = spawnPos;

            bulletObj.transform.rotation =
                Quaternion.identity;

            // Aktifkan bullet
            bulletObj.SetActive(true);

            // Set arah bullet
            Bullet bullet =
                bulletObj.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.SetDirection(shootDirection);

                Debug.Log($"Bullet direction set to: {shootDirection}");
            }
            else
            {
                Debug.LogError("Bullet component not found!");
            }
        }

        Debug.Log("Bullet spawned!");
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
            GameManager.Instance.SetState(GameState.GameOver);
        }
    }
}