using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float currentHP = 100;
    public float speed = 5f;
    // Variabel untuk menyimpan input serangan sebelumnya
    public GameObject bulletPrefab;
    // variabel untuk menentukan posisi spawn peluru
    public Transform bulletSpawnPoint;
    private PlayerInput playerInput;
    private Vector2 moveInput;
    // Variabel untuk melakukan serangan
    private float attackInput;
    // variabe untuk menyimpan input serangan sebelumnya agar bisa mendeteksi perubahan dari tidak menekan ke menekan
    private float previousAttackInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    
    
    void Update()
    {
        if (playerInput == null)
        {
            Debug.LogWarning("PlayerController: playerInput is null");
            return;
        }

        if (GameManager.Instance == null)
        {
            Debug.LogWarning("PlayerController: GameManager.Instance is null");
            return;
        }

        if (GameManager.Instance.currentState != GameState.Playing)
        {
            Debug.Log($"PlayerController blocked because gameState={GameManager.Instance.currentState}");
            return;
        }

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        // Baca input serangan
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

        // if (attackInput > 0)
        // {
        //     Shoot();
        // }
    }
    
   void Shoot()
{
    Debug.Log("Player is shooting!");

    if (BulletPool.Instance == null)
    {
        Debug.LogError("BulletPool not found in scene!");
        return;
    }

    Vector3 spawnPos = bulletSpawnPoint != null ? bulletSpawnPoint.position : transform.position;

    Vector3 mouseScreenPos = Input.mousePosition;
    mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
    mouseWorldPos.z = 0;

    Vector3 shootDirection = (mouseWorldPos - spawnPos).normalized;

    Debug.Log($"Spawn Pos: {spawnPos}, Mouse World Pos: {mouseWorldPos}, Direction: {shootDirection}");

    GameObject bulletObj = BulletPool.Instance.GetBullet(spawnPos);

    //Cek null dulu sebelum dipakai
    if (bulletObj == null)
    {
        Debug.Log("Tidak bisa menembak, ammo habis atau sedang reload!");
        return;
    }

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

    Debug.Log($"Bullet spawned! Pool has {BulletPool.Instance.GetAvailableBulletsCount()} bullets available");
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