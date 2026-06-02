using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("UAS Requirement: Scriptable Object Data")]
    public PlayerData stats; 
  
    private float currentHP;
    private float speed;

    [Header("Weapon Settings")]
    public Transform bulletSpawnPoint;

    private PlayerInput playerInput;
    private Vector2 moveInput;
    private float attackInput;
    private float previousAttackInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

      // Mengambil data dari Scriptable Object agar memenuhi kriteria UAS
        if (stats != null)
        {
            currentHP = stats.maxHP;
            speed = stats.speed;
            Debug.Log($"Data dimuat dari Scriptable Object! HP: {currentHP}, Speed: {speed}");
        }
        else
        {
            Debug.LogError("Scriptable Object 'PlayerData' belum dimasukkan ke Inspector PlayerController!");
           
            currentHP = 100f;
            speed = 5f;
        }
    }
    
    void Update()
    {
        if (playerInput == null) return;
        
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        attackInput = playerInput.actions["Attack"].ReadValue<float>();

        float h = moveInput.x;
        float v = moveInput.y;

      
        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);
        
  
        if (previousAttackInput == 0 && attackInput > 0)
        {
            Shoot();
        }
        
        previousAttackInput = attackInput;
    }
    
    void Shoot()
    {
        Debug.Log("Player is shooting!");

    
        if (ObjectPool.Instance == null)
        {
            Debug.LogError("ObjectPool script belum dipasang di scene!");
            return;
        }

      
        Vector3 spawnPos = bulletSpawnPoint != null ? bulletSpawnPoint.position : transform.position;

      
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0; 
        
    
        Vector3 shootDirection = (mouseWorldPos - spawnPos).normalized;
        
        Debug.Log($"Spawn Pos: {spawnPos}, Mouse World Pos: {mouseWorldPos}, Direction: {shootDirection}");


        GameObject bulletObj = ObjectPool.Instance.GetPooledObject();
        
        if (bulletObj != null)
        {
            bulletObj.transform.position = spawnPos;
            bulletObj.transform.rotation = Quaternion.identity;
            
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.SetDirection(shootDirection);
                Debug.Log($"Bullet direction set to: {shootDirection}");
            }

            bulletObj.SetActive(true);
            Debug.Log("Bullet spawned from pool!");
        }
        else
        {
            Debug.LogWarning("Bank peluru penuh atau belum siap!");
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
          
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
            else
            {
                Debug.LogError("GameManager belum dibuat atau belum ada di Scene!");
            }
        }
    }
}