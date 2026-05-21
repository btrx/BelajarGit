using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
        private float currentHP = 5f;
        private float speed = 5f;
        public GameObject bulletPrefab;
         public Transform bulletSpawnPoint;
        private PlayerInput playerInput;
        private Vector2 moveInput;
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

        // Tampilkan berapa banyak peluru yang masih tersedia di pool (newly added)
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