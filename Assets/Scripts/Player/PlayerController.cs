using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float currentHP = 100;
    public float speed = 5f;
    private PlayerInput playerInput;
    private Vector2 moveInput;

    //fitur serngan
    //variabel untuk menyimpan input serangan sebelumnya
    public GameObject bulletPrefab;
    //variabel untuk menyimpan posisi spawn peluru
    public Transform bulletSpawnPoint;
    //variabel untuk menyimpan input serangan saat ini
    private float attackInput;
    //varoabel untuk menyimpan serangan sebelumnya agar bisa terdeteksi perubahan input serangan
    private float previousAttackInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    
    
    void Update()
    {
        if (playerInput == null) return;
        
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        attackInput = playerInput.actions["Attack"].ReadValue<float>();
        //pake float karena input serangan bisa berupa tombol atau trigger pada controller, yang menghasilkan nilai antara 0 dan 1

        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);

        //untuk cek perubahan input serangan, kita bandingkan input serangan saat ini dengan input serangan sebelumnya
        if(previousAttackInput == 0 && attackInput > 0)
        {
            //jika sebelumnya tidak ada input serangan dan sekarang ada input serangan, maka kita akan menembak
            Shoot();
        }

        //update input serangan sebelumnya
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
        
        GameObject bulletObj = PooledObjects.Instance.GetPooledObject();

        if (bulletObj != null)
        {
            bulletObj.transform.position = spawnPos;
            bulletObj.transform.rotation = Quaternion.identity;
            bulletObj.SetActive(true);

            // Set bullet direction
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