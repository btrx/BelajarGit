using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float currentHP;
    public PlayerData data;
    private PlayerInput playerInput;
    private Vector2 moveInput;  

    public GameObject bulletPrefab;

    public Transform bulletSpawnPoint;
    private float attackInput;

    private float previousAttackInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        currentHP = data.maxHP;
    }
    
    
    void Update()
    {
        
        if (playerInput == null) return;
        
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        attackInput = playerInput.actions["Attack"].ReadValue<float>();

        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * data.moveSpeed * Time.deltaTime);
        if(previousAttackInput == 0 && attackInput > 0)
        {
            Shoot();
        }
        
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

        // // Instantiate bullet
        // GameObject bulletObj = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        
    // Panggil objek dari Pool
    GameObject bulletObj = objectPool.Instance.GetPooledObject();

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
            TakeDamage(1f);
        }
    }

    void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0)
        {
            Debug.Log("Game Over");
            GameManager.Instance.GameOver();
           
        }
    }
    
}