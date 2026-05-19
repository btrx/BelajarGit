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
        if (GameManager.Instance != null && GameManager.Instance.currentState != GameState.Playing)
            return;

        if (playerInput != null)
        {
            moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
            attackInput = playerInput.actions["attack"].ReadValue<float>();
            
            Vector3 direction = new Vector3(moveInput.x, moveInput.y, 0);
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

        // GameObject bulletObj = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        GameObject bulletObj = objectpool.Instance.GetPooledObject();
         if (bulletObj != null)
        {
            // Atur posisi dan rotasi peluru
            bulletObj.transform.position = spawnPos;
            bulletObj.transform.rotation = Quaternion.identity;
            // Aktifkan peluru
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
            TakeDamage(playerData.DamagePerSecond * Time.deltaTime);
            TakeDamage(5f);
        }
    }

    void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0 && GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }   
    }
}