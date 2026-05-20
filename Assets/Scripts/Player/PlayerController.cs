using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    [SerializeField] private PlayerData playerData;

    private float currentHP;
    private float speed;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    private float attackInput;
    private float previousAttackInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerData == null)
        {
            Debug.LogError("PlayerData belum di-assign di Inspector!");
            return;
        }

        currentHP = playerData.maxHP;
        speed = playerData.moveSpeed;
    }

    void Update()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.currentState != GameState.Playing)
            return;

        if (playerInput != null)
        {
            moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

            // PERHATIKAN HURUF BESAR
            attackInput = playerInput.actions["Attack"].ReadValue<float>();

            Vector3 direction =
                new Vector3(moveInput.x, moveInput.y, 0);

            transform.Translate(direction * speed * Time.deltaTime);

            if (previousAttackInput == 0 && attackInput > 0)
            {
                Shoot();
            }

            previousAttackInput = attackInput;
        }
    }

    void Shoot()
    {
        Debug.Log("Player is shooting!");

        if (bulletPrefab == null)
        {
            Debug.LogWarning("Bullet prefab belum diisi!");
            return;
        }

        Vector3 spawnPos =
            bulletSpawnPoint != null
            ? bulletSpawnPoint.position
            : transform.position;

        // Posisi mouse
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z =
            Mathf.Abs(Camera.main.transform.position.z);

        Vector3 mouseWorldPos =
            Camera.main.ScreenToWorldPoint(mouseScreenPos);

        mouseWorldPos.z = 0;

        // Arah peluru
        Vector3 shootDirection =
            (mouseWorldPos - spawnPos).normalized;

        // SPAWN PELURU
        GameObject bulletObj =
            Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

        Bullet bullet = bulletObj.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.SetDirection(shootDirection);
        }
        else
        {
            Debug.LogError("Script Bullet tidak ditemukan!");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision: " + collision.gameObject.name);
    }

    void TakeDamage(float dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0 &&
            GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
}