using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [Header("Shooting Settings")]
    public BulletData bulletData;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireCooldownTime = 0.3f;

    private PlayerInput playerInput;
    private ObjectPoolManager poolManager;
    private float fireCooldown = 0f;
    private Vector2 lastMoveDirection = Vector2.right;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        poolManager = ObjectPoolManager.Instance;

        if (bulletData == null)
            bulletData = Resources.Load<BulletData>("BulletData");
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.CurrentState != GameState.Playing) return;
        if (playerInput == null) return;

        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        if (moveInput.sqrMagnitude > 0.01f)
            lastMoveDirection = moveInput.normalized;

        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;

        if (playerInput.actions["Attack"].WasPressedThisFrame() && fireCooldown <= 0f)
            Shoot();
    }

    private void Shoot()
    {
        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;
        GameObject bulletObj = null;

        if (poolManager != null && poolManager.bulletPrefab != null)
        {
            bulletObj = poolManager.GetBullet();
        }
        else if (bulletPrefab != null)
        {
            bulletObj = Instantiate(bulletPrefab);
        }
        else
        {
            return;
        }

        bulletObj.transform.position = spawnPos;
        bulletObj.transform.rotation = Quaternion.identity;

        Bullet bc = bulletObj.GetComponent<Bullet>();
        if (bc != null)
        {
            if (bulletData != null) bc.bulletData = bulletData;
            bc.shooterTag = gameObject.tag;
            bc.Fire(lastMoveDirection);
        }

        fireCooldown = fireCooldownTime;
    }
}
