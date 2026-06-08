using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Data")]
    public PlayerData playerData;

    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    private float currentHP;
    private float speed;

    void Start()
    {
        currentHP = playerData.maxHP;
        speed = playerData.moveSpeed;

        Debug.Log("Player HP: " + currentHP);
        Debug.Log("Player Speed: " + speed);
    }

    void Update()
    {
        if (GameManager.Instance.currentState != GameState.Playing)
        {
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(h, v, 0);

        transform.Translate(move * speed * Time.deltaTime);

        // Shoot on left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
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

        Debug.Log("Current HP: " + currentHP);

        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log("Press R to Restart");
            GameManager.Instance.GameOver();
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

    void OnDisable()
    {
        Debug.Log("PLAYER DISABLED");
    }

    void OnDestroy()
    {
        Debug.Log("PLAYER DESTROYED");
    }

}