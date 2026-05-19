using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private PlayerData playerData;

    private float currentHP;

     public GameObject bulletPrefab;
    // variabel untuk menentukan posisi spawn peluru
    public Transform bulletSpawnPoint;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    // public GameManager gameManager;

    private float attackInput;
    private float previousAttackInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerData != null)
        {
            currentHP = playerData.maxHP;
        }
        else
        {
            Debug.LogError("PlayerData belum di-assign!");
        }
    }

    void Update()
    {
        if (playerInput == null) return;

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        attackInput = playerInput.actions["Attack"].ReadValue<float>();

        if (previousAttackInput == 0 && attackInput > 0)
        {
            Shoot();
        }

        previousAttackInput = attackInput;

        transform.Translate(new Vector3(moveInput.x, moveInput.y, 0) * playerData.moveSpeed * Time.deltaTime);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(10f * Time.deltaTime); 
        }
    }

    void Shoot()
    {
        Debug.Log("Player is shooting!");
        // Implementasi logika menembak di sini

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
        GameObject bulletObj = ObjectPool.Instance.GetPooledObject();
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
        
        else
        {
            Debug.LogError("Failed to get a bullet from the pool!");
            return;
        }
        
        
    }
    
    

    void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0)
        {
            GameOver();
            // gameManager.GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("MainMenu");
        // currentState = GameState.GameOver;
        Time.timeScale = 1f; 
        Debug.Log("Player Mati");
        Time.timeScale = 0f;
        gameObject.SetActive(false);
    }
}