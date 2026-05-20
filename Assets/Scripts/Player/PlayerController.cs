using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;

    private float currentHP;
    private float speed;
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
        if (GameManager.Instance.currentState != GameState.Playing)
            return;

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
        Debug.Log("Player is shooting");

        // if (BulletPool.Instance == null)
        // {
        //     Debug.LogError("BulletPool not found in scene!");
        //     return;
        // }

        // if (bulletPrefab == null)
        // {
        //     Debug.LogWarning("Bullet prefab not assigned!");
        //     return;
        // }

        Vector3 spawnPos = bulletSpawnPoint != null ? bulletSpawnPoint.position : transform.position;
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0;
        Vector3 shootDirection = (mouseWorldPos - spawnPos).normalized;
        Debug.Log($"Spawn Pos: {spawnPos}, Mouse World Pos: {mouseWorldPos}, Direction: {shootDirection}");

        // GameObject bulletObj = BulletPool.Instance.GetBullet(spawnPos);
        GameObject bulletObj = PooledObjects.Instance.GetPooledObject();

        Bullet bullet = bulletObj.GetComponent<Bullet>();

        // if (bullet != null)
        // {
        //     bullet.SetDirection(shootDirection);
        //     Debug.Log($"Bullet direction set to: {shootDirection}");
        // }
        // else
        // {
        //     Debug.LogError("Bullet component not found on prefab!");
        // }

        if (bulletObj != null)
        {
            // Atur posisi dan rotasi peluru
            bulletObj.transform.position = spawnPos;
            bulletObj.transform.rotation = Quaternion.identity;

            

            // Set arah peluru (Logika aslimu tetap dipertahankan)
            // Bullet bullet = bulletObj.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.SetDirection(shootDirection);
                Debug.Log($"Bullet direction set to: {shootDirection}");
            }
            else
            {
                Debug.LogError("Bullet component not found on prefab!");
            }

            // Aktifkan peluru
            bulletObj.SetActive(true);
        }


        Debug.Log("Bullet spawned!");
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

            GameManager.Instance.BackToMenu();
        }
    }
}