using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [Header("Shooting Settings")]
    public BulletData bulletData;
    public Transform firePoint;

    private PlayerInput playerInput;
    private ObjectPoolManager poolManager;
    private float fireCooldown = 0f;
    private Vector2 lastMoveDirection = Vector2.right;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        poolManager = ObjectPoolManager.Instance;
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
        if (poolManager == null || bulletData == null) return;

        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;

        GameObject bullet = poolManager.GetBullet();
        bullet.transform.position = spawnPos;
        bullet.transform.rotation = Quaternion.identity;

        BulletController bc = bullet.GetComponent<BulletController>();
        if (bc != null)
        {
            bc.bulletData = bulletData;
            bc.Launch(lastMoveDirection);
        }

        fireCooldown = 0.3f;
    }
}
