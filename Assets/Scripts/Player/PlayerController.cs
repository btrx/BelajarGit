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
        if (playerInput == null) return;
        if(GameManager.Instance == null) return;

        if (GameManager.Instance.currentState != GameState.Playing)
            return;

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10f);
        }
        if (playerInput == null) return;
        
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
        if (BulletPool.Instance == null)
        {
            Debug.LogError("BulletPool not found!");
            return;
        }

        Vector3 spawnPos;

        if (bulletSpawnPoint != null)
        {
            spawnPos = bulletSpawnPoint.position;
        }
        else
        {
            spawnPos = transform.position;
        }

        Vector3 mouseScreenPos = Input.mousePosition;

        mouseScreenPos.z =
            Mathf.Abs(Camera.main.transform.position.z);

        Vector3 mouseWorldPos =
            Camera.main.ScreenToWorldPoint(mouseScreenPos);

        mouseWorldPos.z = 0f;

        Vector3 shootDirection =
            (mouseWorldPos - spawnPos).normalized;

        GameObject bulletObj =
            BulletPool.Instance.GetBullet(spawnPos);

        if (bulletObj == null)
        {
            Debug.Log("No bullets available in pool");
            return;
        }

        Bullet bulletScript =
            bulletObj.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDirection(shootDirection);
        }

        Debug.Log(
            "Bullet spawned. Available bullets: " +
            BulletPool.Instance.GetAvailableBulletsCount()
        );
    }    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(10f);
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